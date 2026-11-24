using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [PageIndex]");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        // Optional page index argument (default to first page)
        int pageIndex = 0;
        if (args.Length >= 2 && !int.TryParse(args[1], out pageIndex))
        {
            Console.Error.WriteLine("Error: Invalid page index argument.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Validate requested page index
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page
                Page page = diagram.Pages[pageIndex];

                // Prepare output directory (same folder as input)
                string outputDir = Path.GetDirectoryName(visioPath);
                if (string.IsNullOrEmpty(outputDir))
                {
                    outputDir = Directory.GetCurrentDirectory();
                }

                // Iterate over each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                    {
                        continue;
                    }

                    // Build output PDF file name using the shape ID
                    string outputFileName = $"shape_{shape.ID}.pdf";
                    string outputPath = Path.Combine(outputDir, outputFileName);

                    try
                    {
                        // Export the shape to a PDF file (single-argument overload)
                        shape.ToPdf(outputPath);

                        Console.WriteLine($"Exported Shape ID {shape.ID} to {outputPath}");
                    }
                    catch (Exception shapeEx)
                    {
                        // Log shape-specific errors but continue processing other shapes
                        Console.Error.WriteLine($"Error exporting Shape ID {shape.ID}: {shapeEx.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}