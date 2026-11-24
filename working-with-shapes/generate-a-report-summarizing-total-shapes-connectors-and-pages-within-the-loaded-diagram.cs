using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a diagram file path was provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Initialize counters
                int totalPages = diagram.Pages.Count;
                int totalShapes = 0;
                int totalConnectors = 0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are logically deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Count the shape
                        totalShapes++;

                        // Identify connector shapes by checking the OneD property (1‑D shapes are connectors)
                        if (shape.OneD)
                        {
                            totalConnectors++;
                        }
                    }
                }

                // Output the summary report
                Console.WriteLine("Diagram Summary Report");
                Console.WriteLine("----------------------");
                Console.WriteLine($"Total Pages      : {totalPages}");
                Console.WriteLine($"Total Shapes     : {totalShapes}");
                Console.WriteLine($"Total Connectors : {totalConnectors}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
