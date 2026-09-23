using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Configure font folder (recursive) and default fallback font
            string fontsPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            FontConfigs.SetFontFolder(fontsPath, true);
            FontConfigs.DefaultFontName = "Arial";

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Enumerate fonts used in the diagram and check against installed system fonts
            var installedFonts = new InstalledFontCollection();
            var installedNames = installedFonts.Families
                                                .Select(f => f.Name)
                                                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (Font diagramFont in diagram.Fonts)
            {
                string fontName = diagramFont.Name;
                if (!installedNames.Contains(fontName))
                {
                    Console.WriteLine($"Missing font: {fontName}");
                }
            }

            // Prepare SVG save options with font embedding (default font fallback)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,
                ExportGuideShapes = false,
                SVGFitToViewPort = true,
                DefaultFont = "Arial"
            };

            // Export diagram to SVG
            string outputPath = "output.svg";
            diagram.Save(outputPath, svgOptions);

            Console.WriteLine($"Diagram exported to SVG: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
