using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        try
        {
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare a CSS file to hold class definitions
        string cssFilePath = Path.Combine(outputDir, "styles.css");
        StreamWriter cssWriter;
        try
        {
            cssWriter = new StreamWriter(cssFilePath, false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create CSS file: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Generate a CSS class name based on shape ID
                string cssClass = $"shape-{shape.ID}";

                // Export the shape to an individual SVG file
                string svgFileName = $"shape_{shape.ID}.svg";
                string svgFilePath = Path.Combine(outputDir, svgFileName);

                try
                {
                    // Configure SVG save options (basic settings)
                    SVGSaveOptions svgOptions = new SVGSaveOptions();
                    svgOptions.ExportHiddenPage = false;
                    svgOptions.ExportGuideShapes = false;
                    svgOptions.SVGFitToViewPort = true;

                    // Export the shape
                    shape.ToSvg(svgFilePath, svgOptions);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to export shape ID {shape.ID} to SVG: {ex.Message}");
                    continue;
                }

                // Write a simple CSS rule for the shape
                // (In a real scenario you could extract fill color, line style, etc.)
                cssWriter.WriteLine($".{cssClass} {{");
                cssWriter.WriteLine("    /* Add custom styling here */");
                cssWriter.WriteLine("}");
                cssWriter.WriteLine();
            }
        }

        // Close the CSS writer
        cssWriter.Close();

        // Export the whole diagram to a single SVG file (optional)
        string diagramSvgPath = Path.Combine(outputDir, "diagram.svg");
        try
        {
            SVGSaveOptions diagramSvgOptions = new SVGSaveOptions();
            diagramSvgOptions.ExportHiddenPage = false;
            diagramSvgOptions.ExportGuideShapes = false;
            diagramSvgOptions.SVGFitToViewPort = true;

            // Save the entire diagram as SVG
            diagram.Save(diagramSvgPath, diagramSvgOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to export entire diagram to SVG: {ex.Message}");
        }

        Console.WriteLine("Export completed successfully.");
    }
}