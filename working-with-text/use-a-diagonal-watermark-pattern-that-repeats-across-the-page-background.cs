using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
                diagram.Pages.Add(new Page());

            // Work with the first page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Arial";
            string fontColor = "#CCCCCC"; // light gray
            double fontSizeInInches = 0.5; // approx 36 pt

            // Define spacing for the repeated pattern
            double stepX = pageWidth / 4.0;
            double stepY = pageHeight / 4.0;

            // Add watermark text shapes across the page
            for (double y = 0; y < pageHeight; y += stepY)
            {
                for (double x = 0; x < pageWidth; x += stepX)
                {
                    // Add a text shape at the current position
                    Shape wmShape = page.AddText(x, y, pageWidth, pageHeight,
                                                watermarkText, fontName, fontColor, fontSizeInInches);

                    // Rotate the shape 45 degrees (angle in radians)
                    wmShape.XForm.Angle.Value = (Math.PI / 180.0) * 45.0;

                    // Send the watermark to the back so it doesn't obscure other content
                    wmShape.SendToBack();
                }
            }

            // Save the diagram with the repeated diagonal watermark
            string outputPath = "WatermarkedDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }
}
