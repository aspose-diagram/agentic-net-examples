using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Count total shapes on the page.
                int shapeCount = page.Shapes.Count;

                // Count connector shapes (1‑D shapes) on the page.
                int connectorCount = 0;
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    if (shape.OneD)
                    {
                        connectorCount++;
                    }
                }

                // Retrieve the page name (fallback to empty string if null).
                string pageName = page.Name ?? string.Empty;

                // Output the summary for the current page.
                Console.WriteLine($"Page: {pageName}, Shapes: {shapeCount}, Connectors: {connectorCount}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}