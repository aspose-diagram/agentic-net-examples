using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // User‑defined preferences
                string inputPath = "input.vsdx";          // source diagram
                string outputPath = "output.vsdx";        // destination diagram
                string watermarkText = "CONFIDENTIAL";
                string preferredFont = "Calibri";
                double fontSizePoints = 36;               // size in points

                // Convert points to inches (Visio uses inches for font size)
                double fontSizeInches = fontSizePoints / 72.0;

                // Validate that the requested font is installed on the system
                var fontCollection = new InstalledFontCollection();
                bool fontExists = fontCollection.Families
                    .Any(f => string.Equals(f.Name, preferredFont, StringComparison.OrdinalIgnoreCase));

                if (!fontExists)
                {
                    throw new Exception($"The font \"{preferredFont}\" is not installed on this machine.");
                }

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply the watermark to each page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add the watermark text covering the whole page
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, size(inches)
                    Shape watermarkShape = page.AddText(pinX, pinY, pageWidth, pageHeight,
                                                        watermarkText, preferredFont, "#808080", fontSizeInches);

                    // Optional: send the watermark to the back so it does not obscure other shapes
                    watermarkShape.SendToBack();
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }