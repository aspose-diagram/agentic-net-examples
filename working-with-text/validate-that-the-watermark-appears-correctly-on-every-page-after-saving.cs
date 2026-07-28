using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";
            const string watermarkText = "CONFIDENTIAL";

            // Load the original diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Add watermark to every page
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Font size in inches (e.g., 0.5 inches ≈ 36 points)
                    double fontSizeInches = 0.5;

                    // Add the watermark text covering the full page area
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 watermarkText, "Arial", "#CCCCCC", fontSizeInches);
                }

                // Save the diagram with watermarks
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Reload the saved diagram to verify watermarks
            using (Diagram savedDiagram = new Diagram(outputPath))
            {
                foreach (Page page in savedDiagram.Pages)
                {
                    bool watermarkFound = false;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Get plain text of the shape
                        string shapeText = shape.Text.Value.Text;

                        if (!string.IsNullOrWhiteSpace(shapeText) &&
                            shapeText.Contains(watermarkText, StringComparison.OrdinalIgnoreCase))
                        {
                            watermarkFound = true;
                            break;
                        }
                    }

                    if (!watermarkFound)
                    {
                        throw new Exception($"Watermark not found on page '{page.Name}' (ID: {page.ID}).");
                    }
                }

                Console.WriteLine("Validation successful: Watermark appears on every page.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
