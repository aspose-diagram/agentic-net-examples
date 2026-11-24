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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ---------- Export to HTML with hidden pages ----------
            {
                // Configure HTML save options to include hidden pages.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    ExportHiddenPage = true
                };

                // Build output file path.
                string htmlPath = Path.Combine(outputDir, "output.html");

                // Save the diagram as HTML.
                diagram.Save(htmlPath, htmlOptions);
                Console.WriteLine($"HTML exported to: {htmlPath}");
            }

            // ---------- Export to PNG with hidden pages ----------
            {
                // Configure image save options for PNG format.
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    ExportHiddenPage = true
                };

                // Build output file path.
                string pngPath = Path.Combine(outputDir, "output.png");

                // Save the diagram as PNG.
                diagram.Save(pngPath, pngOptions);
                Console.WriteLine($"PNG exported to: {pngPath}");
            }

            // ---------- Export to SVG with hidden pages ----------
            {
                // Configure SVG save options.
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    ExportHiddenPage = true
                };

                // Build output file path.
                string svgPath = Path.Combine(outputDir, "output.svg");

                // Save the diagram as SVG.
                diagram.Save(svgPath, svgOptions);
                Console.WriteLine($"SVG exported to: {svgPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}