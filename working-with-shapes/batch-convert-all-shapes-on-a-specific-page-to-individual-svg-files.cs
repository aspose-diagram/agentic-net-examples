using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the Visio file path.
        Console.Write("Enter the path to the Visio file: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists.
        if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
        {
            Console.Error.WriteLine("Error: The specified Visio file does not exist.");
            return;
        }

        // Prompt user for the page index (0‑based).
        Console.Write("Enter the page index (0‑based) to export shapes from: ");
        string pageIndexInput = Console.ReadLine();
        if (!int.TryParse(pageIndexInput, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index.");
            return;
        }

        // Prepare output directory.
        string outputDir = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "SvgOutput");
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: Unable to create output directory. {ex.Message}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate page index range.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine("Error: Page index exceeds the number of pages in the document.");
                    return;
                }

                // Retrieve the target page.
                Page page = diagram.Pages[pageIndex];

                // Configure SVG save options (common for all shapes).
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // These properties are of type bool, not Aspose BOOL.
                    SVGFitToViewPort = true,          // Fit SVG to viewport.
                    ExportGuideShapes = false,        // Do not export guide shapes.
                    IsExportComments = false,         // Do not export comments.
                    SaveFormat = SaveFileFormat.Svg   // Explicitly set format.
                };

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for each shape.
                    string shapeFileName = $"Shape_{shape.ID}.svg";
                    string shapeFilePath = Path.Combine(outputDir, shapeFileName);

                    try
                    {
                        // Export the shape to SVG using the ToSvg method.
                        shape.ToSvg(shapeFilePath, svgOptions);
                        Console.WriteLine($"Exported shape ID {shape.ID} to {shapeFileName}");
                    }
                    catch (Exception exShape)
                    {
                        Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {exShape.Message}");
                        // Continue with next shape.
                    }
                }

                Console.WriteLine("Batch SVG export completed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}