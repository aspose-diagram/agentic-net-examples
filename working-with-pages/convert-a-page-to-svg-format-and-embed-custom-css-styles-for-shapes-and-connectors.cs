using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure SVG save options for a single page (first page, zero‑based)
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.ExportHiddenPage = false;      // Do not export hidden pages
                svgOptions.ExportGuideShapes = false;    // Do not export guide shapes
                svgOptions.SVGFitToViewPort = true;      // Fit SVG to viewport
                svgOptions.PageIndex = 0;                // Export the first page

                // Save the selected page to SVG
                diagram.Save(outputPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during SVG conversion: {ex.Message}");
            return;
        }

        // Embed custom CSS into the generated SVG file
        try
        {
            string svgContent = File.ReadAllText(outputPath);

            // Define custom CSS styles for shapes and connectors
            string customCss = @"
    <style type=""text/css"">
        .shape { stroke:#000000; fill:#FFFFFF; }
        .connector { stroke:#FF0000; fill:none; }
    </style>";

            // Locate the position after the opening <svg> tag
            int insertPos = svgContent.IndexOf('>');
            if (insertPos != -1)
            {
                // Insert CSS right after the opening tag
                svgContent = svgContent.Insert(insertPos + 1, customCss);
                File.WriteAllText(outputPath, svgContent);
            }
            else
            {
                Console.Error.WriteLine("Unable to locate <svg> tag in the generated file.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error embedding CSS into SVG: {ex.Message}");
        }
    }
}
