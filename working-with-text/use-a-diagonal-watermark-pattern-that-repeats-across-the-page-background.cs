using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Determine the maximum existing page ID to assign a unique ID to the background page
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a background page and mark it as a background canvas
            Page backgroundPage = new Page();
            backgroundPage.ID = maxPageId + 1;
            backgroundPage.Name = "Background";
            backgroundPage.Background = BOOL.True;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to the background page
            foregroundPage.BackPage = backgroundPage;

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            string fontColor = "#CCCCCC"; // Light gray
            double fontSizeInInches = 0.5; // Approx. 36 points
            double rotationAngle = 45; // Degrees
            double tileSpacing = 2.0; // Inches between watermark instances

            // Tile the watermark across the background page
            for (double y = 0; y < pageHeight; y += tileSpacing)
            {
                for (double x = 0; x < pageWidth; x += tileSpacing)
                {
                    // Add a text shape covering the whole page (width/height set to page size)
                    // AddText returns a Shape object directly, so we capture it in 'shape'
                    Shape shape = backgroundPage.AddText(
                        x,                     // PinX (center X)
                        y,                     // PinY (center Y)
                        pageWidth,             // Width of the text shape
                        pageHeight,            // Height of the text shape
                        watermarkText,
                        fontName,
                        fontColor,
                        fontSizeInInches);

                    // Rotate the text diagonally
                    shape.XForm.Angle.Value = rotationAngle;

                    // Send the watermark shape to the back so it appears behind other content
                    shape.SendToBack();

                    // Make the watermark non‑selectable
                    shape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Save the diagram with the tiled diagonal watermark
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}