using System;
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

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the exported SVG file
                string outputSvgPath = "shape_output.svg";

                // Configure font folder (required before loading diagram)
                // Adjust the path to the system fonts folder on the target machine
                FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first shape that contains text
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape with text found in the diagram.");
                    return;
                }

                // Export the shape to SVG
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                targetShape.ToSvg(outputSvgPath, svgOptions);
                Console.WriteLine($"Shape exported to SVG: {outputSvgPath}");

                // Verify that all fonts used in the diagram are installed on the system
                var installedFonts = new InstalledFontCollection();

                bool allDiagramFontsAvailable = true;
                foreach (Aspose.Diagram.Font diagramFont in diagram.Fonts)
                {
                    bool found = installedFonts.Families.Any(f => f.Name.Equals(diagramFont.Name, StringComparison.OrdinalIgnoreCase));
                    if (!found)
                    {
                        allDiagramFontsAvailable = false;
                        Console.WriteLine($"Missing diagram font: {diagramFont.Name}");
                    }
                }

                // Verify that the fonts used in the shape's text runs are installed
                bool allShapeFontsAvailable = true;
                foreach (Aspose.Diagram.Char ch in targetShape.Chars)
                {
                    string fontName = ch.FontName.Value;
                    bool found = installedFonts.Families.Any(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
                    if (!found)
                    {
                        allShapeFontsAvailable = false;
                        Console.WriteLine($"Missing font in shape text: {fontName}");
                    }
                }

                // Report verification result
                if (allDiagramFontsAvailable && allShapeFontsAvailable)
                {
                    Console.WriteLine("All fonts used by the diagram and the shape are available on the system.");
                }
                else
                {
                    throw new Exception("One or more required fonts are missing. See console output for details.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }