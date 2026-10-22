using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Count total pages.
                int pageCount = diagram.Pages.Count;

                int totalShapes = 0;
                int totalConnectors = 0;

                // Iterate through each page and its shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        totalShapes++;

                        // Identify connector shapes (1‑D shapes).
                        if (shape.OneD)
                            totalConnectors++;
                    }
                }

                // Output the summary report.
                Console.WriteLine("Diagram Summary Report");
                Console.WriteLine("----------------------");
                Console.WriteLine($"Total Pages      : {pageCount}");
                Console.WriteLine($"Total Shapes     : {totalShapes}");
                Console.WriteLine($"Total Connectors : {totalConnectors}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}