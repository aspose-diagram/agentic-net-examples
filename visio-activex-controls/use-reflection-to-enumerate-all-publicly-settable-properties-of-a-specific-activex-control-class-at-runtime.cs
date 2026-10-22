using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioDiagramPath>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the first shape that contains an ActiveX control.
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

            // Retrieve the concrete ActiveX control instance.
            ActiveXControl control = activeXShape.ActiveXControl;
            Type controlType = control.GetType();

            Console.WriteLine($"ActiveX Control Type: {controlType.FullName}");
            Console.WriteLine("Publicly settable properties:");

            // Use reflection to enumerate public instance properties that have a public setter.
            PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo prop in properties)
            {
                MethodInfo setMethod = prop.GetSetMethod();
                if (setMethod != null && setMethod.IsPublic)
                {
                    // Retrieve current value safely (may throw for write‑only properties).
                    object value;
                    try
                    {
                        value = prop.GetValue(control);
                    }
                    catch
                    {
                        value = "<unreadable>";
                    }

                    Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name}) = {value}");
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or reflection errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}