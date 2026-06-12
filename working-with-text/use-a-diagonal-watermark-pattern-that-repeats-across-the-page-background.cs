using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new blank diagram
        using (Diagram diagram = new Diagram())
        {
            // Ensure there is at least one foreground page
            Page foregroundPage = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;

            // Create a background page
            Page backgroundPage = new Page();
            backgroundPage.Name = "Background";
            backgroundPage.Background = BOOL.True;
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Add the background page to the diagram and link it to the foreground page
            diagram.Pages.Add(backgroundPage);
            foregroundPage.BackPage = backgroundPage;

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#CCCCCC"; // Light gray
            double fontSizeInches = 0.5;   // Approx. 36 points (0.5 inch)

            // Spacing between watermark instances
            double stepX = pageWidth / 4;   // Four columns
            double stepY = pageHeight / 4;  // Four rows

            // Loop to place watermark text across the background page
            for (double y = stepY / 2; y < pageHeight; y += stepY)
            {
                for (double x = stepX / 2; x < pageWidth; x += stepX)
                {
                    // Add a text shape at the calculated position
                    Shape wmShape = backgroundPage.AddText(
                        pinX: x,
                        pinY: y,
                        width: 0,
                        height: 0,
                        text: watermarkText,
                        fontName: fontName,
                        fontColor: fontColor,
                        size: fontSizeInches);

                    // Rotate the text 45 degrees (in radians)
                    wmShape.TextXForm.TxtAngle.Value = (Math.PI / 180) * 45;

                    // Send the watermark to the back so it stays behind other content
                    wmShape.SendToBack();

                    // Make the watermark non‑selectable
                    wmShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Save the diagram with the repeated diagonal watermark
            diagram.Save("WatermarkedDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }
}
