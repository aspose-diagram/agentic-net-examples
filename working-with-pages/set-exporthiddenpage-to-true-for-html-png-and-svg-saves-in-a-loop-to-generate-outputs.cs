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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (use input file directory if not provided)
        string outputDir = args.Length > 1 ? args[1] : Path.GetDirectoryName(inputPath);
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ---------- Export to HTML with hidden pages ----------
            {
                // Create HTML save options and enable hidden page export
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.ExportHiddenPage = true;

                // Define output file path
                string htmlPath = Path.Combine(outputDir, "output.html");

                // Save diagram as HTML
                diagram.Save(htmlPath, htmlOptions);
                Console.WriteLine($"HTML saved to: {htmlPath}");
            }

            // ---------- Export to PNG with hidden pages ----------
            {
                // Create image save options for PNG and enable hidden page export
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                pngOptions.ExportHiddenPage = true;

                // Define output file path
                string pngPath = Path.Combine(outputDir, "output.png");

                // Save diagram as PNG
                diagram.Save(pngPath, pngOptions);
                Console.WriteLine($"PNG saved to: {pngPath}");
            }

            // ---------- Export to SVG with hidden pages ----------
            {
                // Create SVG save options and enable hidden page export
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.ExportHiddenPage = true;

                // Define output file path
                string svgPath = Path.Combine(outputDir, "output.svg");

                // Save diagram as SVG
                diagram.Save(svgPath, svgOptions);
                Console.WriteLine($"SVG saved to: {svgPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}