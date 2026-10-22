using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Configure a fallback font before loading the diagram.
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Optional: enumerate fonts used in the diagram and warn about missing fonts.
            InstalledFontCollection installedFonts = new InstalledFontCollection();
            foreach (Font diagramFont in diagram.Fonts)
            {
                bool isInstalled = installedFonts.Families
                    .Any(f => string.Equals(f.Name, diagramFont.Name, StringComparison.OrdinalIgnoreCase));

                if (!isInstalled)
                {
                    Console.Error.WriteLine($"Warning: Font \"{diagramFont.Name}\" used in diagram is not installed. Fallback will be applied.");
                }
            }

            // Prepare SVG save options with font embedding enabled where possible.
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,          // Do not export hidden pages.
                ExportGuideShapes = false,         // Do not export guide shapes.
                IsExportComments = false,          // Do not export comments.
                SVGFitToViewPort = true,           // Fit SVG to viewport.
                ExportElementAsRectTag = true,     // Export elements as <rect> tags.
                SaveFormat = SaveFileFormat.Svg   // Specify SVG format.
            };

            // Save the diagram as SVG.
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}
