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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Get the active page
            Page page = diagram.ActivePage;

            // Add a sample shape (Rectangle) with some text
            long rectId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape rectShape = page.Shapes.GetShape((int)rectId);
            rectShape.Text.Value.Clear();
            rectShape.Text.Value.Add(new Txt("Sample Text"));

            // Determine page dimensions for full‑page watermark
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Center position for the watermark
            double centerX = pageWidth / 2.0;
            double centerY = pageHeight / 2.0;

            // Add a watermark text shape covering the whole page
            // Font size is in inches (e.g., 0.5 inches ≈ 36 points)
            Shape watermark = page.AddText(centerX, centerY, pageWidth, pageHeight,
                                          "WATERMARK", "Arial", "#CCCCCC", 0.5);

            // Send the watermark to the back so it does not obscure other shape text
            watermark.SendToBack();

            // Save the diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
