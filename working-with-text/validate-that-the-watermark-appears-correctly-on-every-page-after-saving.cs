using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        const string watermark = "CONFIDENTIAL";
        const string outputPath = "watermarked.vsdx";

        // Create a new diagram and add a watermark to each page
        using (Diagram diagram = new Diagram())
        {
            foreach (Page page in diagram.Pages)
            {
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page text shape as watermark
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                page.AddText(0, 0, pageWidth, pageHeight, watermark, "Arial", "#808080", 0.5);
            }

            // Save the diagram with the watermark
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        // Load the saved diagram and verify the watermark exists on every page
        using (Diagram loadedDiagram = new Diagram(outputPath))
        {
            foreach (Page page in loadedDiagram.Pages)
            {
                bool watermarkFound = false;

                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text from the shape
                    string shapeText = shape.Text.Value.Text;

                    if (!string.IsNullOrEmpty(shapeText) && shapeText == watermark)
                    {
                        watermarkFound = true;
                        break;
                    }
                }

                if (!watermarkFound)
                {
                    throw new Exception($"Watermark not found on page ID {page.ID}");
                }
            }
        }

        Console.WriteLine("Watermark validation passed on all pages.");
    }
}
