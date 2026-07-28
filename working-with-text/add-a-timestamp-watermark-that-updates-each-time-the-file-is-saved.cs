using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            // Replace the path with your actual file location.
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Current timestamp to be used as watermark text.
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Iterate through all pages and add or update the watermark.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Search for an existing watermark shape by its name.
                Shape watermarkShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Watermark", StringComparison.OrdinalIgnoreCase))
                    {
                        watermarkShape = shape;
                        break;
                    }
                }

                if (watermarkShape != null)
                {
                    // Update the existing watermark text.
                    watermarkShape.Text.Value.Clear();
                    watermarkShape.Text.Value.Add(new Txt(timestamp));
                }
                else
                {
                    // Add a new text shape that covers the whole page.
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches).
                    Shape newShape = page.AddText(
                        0,                     // pinX (left)
                        0,                     // pinY (bottom)
                        pageWidth,             // width
                        pageHeight,            // height
                        timestamp,             // watermark text
                        "Calibri",             // font name
                        "#a5a5a5",             // light gray color
                        0.25);                 // font size in inches (~18 pt)

                    // Assign a recognizable name to the shape for future updates.
                    newShape.Name = "Watermark";
                    newShape.NameU = "Watermark";
                }
            }

            // Save the diagram. The watermark will reflect the current timestamp each time this code runs.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
