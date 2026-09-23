using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be processed
        string diagramPath = "input.vsdx";

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // The custom property value we are looking for
        string targetValue = "DesiredValue";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            Console.WriteLine($"Masters containing a custom property with value \"{targetValue}\":");

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                bool masterMatches = false; // flag to avoid duplicate output

                // Each master can contain multiple shapes; inspect each shape's custom properties
                foreach (Shape shape in master.Shapes)
                {
                    // Iterate over the shape's custom properties (Props)
                    foreach (Prop prop in shape.Props)
                    {
                        // Compare the property's value with the target value
                        if (prop.Value != null && prop.Value.Val == targetValue)
                        {
                            // Output master information once a matching property is found
                            Console.WriteLine($"- Master ID: {master.ID}, Name: {master.Name}");
                            masterMatches = true;
                            break; // exit inner Prop loop
                        }
                    }

                    if (masterMatches) break; // exit shape loop once a match is found
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }

        // Optional: keep console window open when run outside IDE
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}