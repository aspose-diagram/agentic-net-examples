using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <pageIndex> <outputSvgPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
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
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the requested page index is within range.
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} page(s).");
                    return;
                }

                // Configure SVG save options to export only the specified page.
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    PageIndex = pageIndex,          // zero‑based page index
                    ExportGuideShapes = false,      // omit guide shapes
                    SVGFitToViewPort = true         // preserve viewbox
                    // PageCount property is not available in this version; omitting it exports the selected page.
                };

                // Save the selected page as SVG.
                diagram.Save(outputPath, svgOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        try
        {
            // Read the generated SVG content.
            string svgContent = File.ReadAllText(outputPath);

            // Define custom CSS to style shapes and connectors.
            string customCss = @"
<style type=""text/css"">
    /* Shape styling */
    .shape { fill:#e0f7fa; stroke:#006064; stroke-width:1.5; }
    /* Connector styling */
    .connector { stroke:#ff6f00; stroke-width:2; fill:none; }
</style>";

            // Insert the CSS block right after the opening <svg> tag.
            int svgTagEnd = svgContent.IndexOf('>');
            if (svgTagEnd != -1)
            {
                svgContent = svgContent.Insert(svgTagEnd + 1, "\n" + customCss + "\n");
            }
            else
            {
                Console.Error.WriteLine("Warning: Unable to locate opening <svg> tag. CSS not inserted.");
            }

            // Write the modified SVG back to the output file.
            File.WriteAllText(outputPath, svgContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while embedding CSS into SVG: {ex.Message}");
        }
    }
}
