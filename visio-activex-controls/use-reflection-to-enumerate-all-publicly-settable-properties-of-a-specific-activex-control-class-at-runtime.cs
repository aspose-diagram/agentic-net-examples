using System;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file containing ActiveX controls
                string diagramPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes to find the first shape with an ActiveX control
                Shape activeXShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ActiveXControl != null)
                        {
                            activeXShape = shape;
                            break;
                        }
                    }
                    if (activeXShape != null) break;
                }

                if (activeXShape == null)
                {
                    Console.WriteLine("No shape with an ActiveX control was found in the diagram.");
                    return;
                }

                // Retrieve the concrete ActiveX control instance
                ActiveXControl control = activeXShape.ActiveXControl;
                Type controlType = control.GetType();

                Console.WriteLine($"ActiveX Control Type: {controlType.FullName}");
                Console.WriteLine("Publicly settable properties:");

                // Get all public instance properties that have a public setter
                PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo prop in properties)
                {
                    if (prop.CanWrite && prop.GetSetMethod(/*nonPublic*/ false) != null)
                    {
                        // Retrieve current value safely (may be null)
                        object value = null;
                        try
                        {
                            value = prop.GetValue(control);
                        }
                        catch
                        {
                            // Ignored – some properties may throw if not initialized
                        }

                        Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name}) = {value ?? "null"}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }