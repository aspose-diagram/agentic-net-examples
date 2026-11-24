using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <PageIndex (0‑based)>");
            return;
        }

        string visioPath = args[0];
        string pageIndexArg = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found – {visioPath}");
            return;
        }

        // Guard: parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Page index must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Guard: ensure the requested page exists.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the specific page.
                Page page = diagram.Pages[pageIndex];

                // Prepare SVG save options (common for all shapes).
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.ExportHiddenPage = false;
                svgOptions.ExportGuideShapes = false;
                svgOptions.SVGFitToViewPort = true;

                // Output directory: create a subfolder named after the page index.
                string outputDir = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", $"Page_{pageIndex}_SVG");
                Directory.CreateDirectory(outputDir);

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for each shape.
                    string shapeFileName = $"Shape_{shape.ID}.svg";
                    string outputPath = Path.Combine(outputDir, shapeFileName);

                    try
                    {
                        // Export the shape to an individual SVG file.
                        shape.ToSvg(outputPath, svgOptions);
                        Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
                    }
                    catch (Exception exShape)
                    {
                        // Log shape‑specific errors but continue processing other shapes.
                        Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {exShape.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or processing.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}