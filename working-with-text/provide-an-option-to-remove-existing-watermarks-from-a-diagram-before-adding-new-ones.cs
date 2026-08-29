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

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and remove shapes that are identified as watermarks
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs to delete (cannot modify collection while iterating)
                var idsToDelete = new System.Collections.Generic.List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // Identify watermark by checking its text content (customize condition as needed)
                    string shapeText = shape.Text.Value.ToString();
                    if (!string.IsNullOrWhiteSpace(shapeText) && shapeText.Contains("Watermark", StringComparison.OrdinalIgnoreCase))
                    {
                        idsToDelete.Add(shape.ID);
                    }
                }

                // Mark identified shapes as deleted
                foreach (long id in idsToDelete)
                {
                    Shape shape = page.Shapes.GetShape(id);
                    shape.Del = BOOL.True; // Mark shape for deletion
                }
            }

            // Add a new watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Watermark text and styling
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#A0A0A0"; // Light gray in hex
                double fontSizeInPoints = 36; // 36 pt
                double fontSizeInInches = fontSizeInPoints / 72.0; // Convert points to inches

                // Add the watermark as a full‑page text shape
                page.AddText(
                    centerX,               // PinX (center X)
                    centerY,               // PinY (center Y)
                    pageWidth,             // Width (full page)
                    pageHeight,            // Height (full page)
                    watermarkText,         // Text
                    fontName,              // Font name
                    fontColor,             // Font color (hex)
                    fontSizeInInches       // Font size (in inches)
                );
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
