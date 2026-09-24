using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        const string outputPath = "watermarked.vsdx";
        const string watermarkText = "CONFIDENTIAL";

        // Create a new diagram and add a watermark to each page
        using (Diagram diagram = new Diagram())
        {
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add watermark text covering the whole page
                // pinX and pinY are set to 0 to start at the lower‑left corner
                // fontSize is in inches (0.5 inches ≈ 36 points)
                page.AddText(0, 0, pageWidth, pageHeight, watermarkText, "Arial", "#808080", 0.5);
            }

            // Save the diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }

        // Load the saved diagram and verify the watermark on every page
        using (Diagram loadedDiagram = new Diagram(outputPath))
        {
            foreach (Page page in loadedDiagram.Pages)
            {
                bool watermarkFound = false;

                foreach (Shape shape in page.Shapes)
                {
                    // Get the concatenated plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains(watermarkText))
                    {
                        watermarkFound = true;
                        break;
                    }
                }

                if (!watermarkFound)
                {
                    throw new Exception($"Watermark \"{watermarkText}\" not found on page \"{page.Name}\".");
                }
            }
        }

        Console.WriteLine("Watermark validated on all pages successfully.");
    }
}
