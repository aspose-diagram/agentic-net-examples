using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: diagram path, property name, property value
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <propertyName> <propertyValue>");
            return;
        }

        string diagramPath = args[0];
        string targetPropName = args[1];
        string targetPropValue = args[2];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Examine each custom property (Prop) of the shape
                    foreach (Prop prop in shape.Props)
                    {
                        // Check for matching property name and value
                        if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                        {
                            // Output the shape's ID (long) to the console
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            // No need to check other properties of this shape
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}