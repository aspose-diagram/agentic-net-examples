using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and the custom property value to filter by.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <customPropertyValue>");
            return;
        }

        string diagramPath = args[0];
        string targetValue = args[1];

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

            bool anyMatch = false;

            // Iterate through all masters in the diagram.
            foreach (Master master in diagram.Masters)
            {
                bool masterMatches = false;

                // Iterate through shapes that belong to the master.
                foreach (Shape shape in master.Shapes)
                {
                    // Ensure the shape has a Props collection before iterating.
                    if (shape.Props == null) continue;

                    // Iterate through custom properties of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Compare the property's value with the target value (case‑insensitive).
                        if (string.Equals(prop.Value.Val, targetValue, StringComparison.OrdinalIgnoreCase))
                        {
                            masterMatches = true;
                            break;
                        }
                    }

                    if (masterMatches) break;
                }

                if (masterMatches)
                {
                    // Output matching master information.
                    Console.WriteLine($"Master Name: {master.Name}, Master ID: {master.ID}");
                    anyMatch = true;
                }
            }

            if (!anyMatch)
            {
                Console.WriteLine("No masters found with the specified custom property value.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}