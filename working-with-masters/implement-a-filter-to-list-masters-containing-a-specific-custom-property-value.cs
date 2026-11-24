using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <customPropertyValue>");
            return;
        }

        string diagramPath = args[0];
        string targetValue = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all masters in the diagram.
            foreach (Master master in diagram.Masters)
            {
                bool containsTarget = false;

                // Each master can contain one or more shapes that define its custom properties.
                // Iterate through the shapes of the master.
                foreach (Shape shape in master.Shapes)
                {
                    // Guard against null Props collection.
                    if (shape.Props == null)
                        continue;

                    // Examine each custom property (Prop) of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Ensure the property value object is not null before accessing Val.
                        if (prop.Value != null && prop.Value.Val == targetValue)
                        {
                            containsTarget = true;
                            break;
                        }
                    }

                    if (containsTarget)
                        break;
                }

                // If the master has a matching custom property, output its name.
                if (containsTarget)
                {
                    Console.WriteLine($"Master Name: {master.Name}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}