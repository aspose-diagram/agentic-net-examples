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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputSvgFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply a page‑level theme to each page.
                foreach (Page page in diagram.Pages)
                {
                    // Set a preset theme (example: Bubble) and a variant.
                    page.PresetTheme = PresetThemeValue.Bubble;
                    page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                }

                // Configure SVG export options.
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    // Fit the SVG to the viewport.
                    SVGFitToViewPort = true,
                    // Do not export guide shapes.
                    ExportGuideShapes = false,
                    // Do not export comments.
                    IsExportComments = false,
                    // Export all pages (no PageCount restriction).
                    // PageIndex defaults to 0 (first page) if not set; leaving it unset exports all pages.
                    SaveFormat = SaveFileFormat.Svg
                };

                // Save the diagram as SVG using the configured options.
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"Diagram exported to SVG successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}