using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and page index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse the page index (zero‑based)
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate the requested page index
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                return;
            }

            // Retrieve the specific page
            Page page = diagram.Pages[pageIndex];

            // Iterate over each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Build output PDF file name using shape ID
                string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{pageIndex}_Shape{shape.ID}.pdf";
                string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", outputFileName);

                try
                {
                    // Export the individual shape to PDF
                    shape.ToPdf(outputPath);
                    Console.WriteLine($"Exported Shape ID {shape.ID} to {outputPath}");
                }
                catch (Exception ex)
                {
                    // Report errors for this shape but continue processing others
                    Console.Error.WriteLine($"Failed to export Shape ID {shape.ID}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while loading or processing the diagram
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}