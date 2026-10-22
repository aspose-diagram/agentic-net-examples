using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: at least the input diagram path is required.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [shapeId1 shapeId2 ...]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse optional shape IDs from the command line.
        HashSet<long> selectedShapeIds = new HashSet<long>();
        for (int i = 1; i < args.Length; i++)
        {
            if (long.TryParse(args[i], out long id))
            {
                selectedShapeIds.Add(id);
            }
            else
            {
                Console.Error.WriteLine($"Warning: Invalid shape ID '{args[i]}' ignored.");
            }
        }

        // Prepare output directory.
        string outputDir = Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "ExportedSvg");
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure SVG save options (default settings can be adjusted if needed).
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Fit the exported SVG to the viewport.
                    SVGFitToViewPort = true,
                    // Do not export guide shapes.
                    ExportGuideShapes = false,
                    // Do not export comments.
                    IsExportComments = false
                    // PageIndex and PageCount are omitted because they are not required for shape export
                };

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // If specific shape IDs were provided, filter accordingly.
                        if (selectedShapeIds.Count > 0 && !selectedShapeIds.Contains(shape.ID))
                            continue;

                        // Build the output file name using the shape's ID.
                        string outputPath = Path.Combine(outputDir, $"{shape.ID}.svg");

                        try
                        {
                            // Export the shape to SVG.
                            shape.ToSvg(outputPath, svgOptions);
                            Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
                        }
                        catch (Exception exportEx)
                        {
                            Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {exportEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}