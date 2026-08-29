using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output SVG file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.svg";
        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Set the page theme (e.g., Bubble) and a variant
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,          // Do not export hidden pages
                ExportGuideShapes = false,         // Skip guide shapes
                SVGFitToViewPort = true,           // Fit content to viewport
                ExportElementAsRectTag = true      // Use <rect> for shape elements
            };

            // Save the themed diagram as an SVG file
            diagram.Save(outputPath, svgOptions);
            Console.WriteLine($"SVG export completed: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}