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
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <ActiveXControlClassName>");
            return;
        }

        string diagramPath = args[0];
        string targetControlClassName = args[1];

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

            bool controlFound = false;

            // Iterate through all pages and shapes to locate ActiveX controls.
            foreach (Page page in diagram.Pages)
            {
                // Use zero‑based index for shapes collection.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Check if the shape contains an ActiveX control.
                    if (shape.ActiveXControl != null)
                    {
                        // Determine the runtime type of the control.
                        object activeXControl = shape.ActiveXControl;
                        Type controlType = activeXControl.GetType();

                        // Compare the control type name with the requested class name.
                        if (string.Equals(controlType.Name, targetControlClassName, StringComparison.OrdinalIgnoreCase))
                        {
                            controlFound = true;
                            // Use page.ID (long) instead of non‑existent PageID.
                            Console.WriteLine($"ActiveX Control found on Page ID {page.ID}, Shape Index {i}: {controlType.Name}");
                            Console.WriteLine("Publicly settable properties:");

                            // Use reflection to enumerate public instance properties that have a public setter.
                            PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            foreach (PropertyInfo prop in properties)
                            {
                                MethodInfo setMethod = prop.GetSetMethod();
                                if (setMethod != null && setMethod.IsPublic)
                                {
                                    Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");
                                }
                            }

                            Console.WriteLine(); // Blank line for readability.
                        }
                    }
                }
            }

            if (!controlFound)
            {
                Console.WriteLine($"No ActiveX control of type '{targetControlClassName}' was found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or reflection errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}