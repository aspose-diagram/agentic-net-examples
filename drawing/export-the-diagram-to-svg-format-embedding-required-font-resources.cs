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

        // Guard: ensure the input file exists.
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
                // ------------------------------------------------------------
                // Validate fonts used in the diagram against installed system fonts.
                // ------------------------------------------------------------
                InstalledFontCollection installedFonts = new InstalledFontCollection();

                // Iterate over each font referenced by the diagram.
                foreach (Font font in diagram.Fonts)
                {
                    bool isInstalled = installedFonts.Families
                        .Any(f => string.Equals(f.Name, font.Name, StringComparison.OrdinalIgnoreCase));

                    if (!isInstalled)
                    {
                        Console.WriteLine($"Warning: Font \"{font.Name}\" is not installed on this system.");
                    }
                }

                // Set a fallback default font for any missing fonts.
                // This does not embed fonts but ensures a substitute is used.
                FontConfigs.DefaultFontName = "Arial";

                // ------------------------------------------------------------
                // Configure SVG save options.
                // ------------------------------------------------------------
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                svgOptions.SaveFormat = SaveFileFormat.Svg;          // Specify SVG format.
                svgOptions.SVGFitToViewPort = true;                // Fit SVG to viewport.
                svgOptions.ExportHiddenPage = false;               // Do not export hidden pages.
                svgOptions.ExportGuideShapes = false;              // Do not export guide shapes.
                svgOptions.IsExportComments = false;               // Do not export comments.

                // ------------------------------------------------------------
                // Export the diagram to SVG.
                // ------------------------------------------------------------
                diagram.Save(outputPath, svgOptions);
                Console.WriteLine($"Diagram successfully exported to SVG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error during SVG export: {ex.Message}");
        }
    }
}