using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // User‑defined palette: map page ID to a hex color string
            var palette = new Dictionary<int, string>
            {
                { 0, "#FF0000" }, // Red for page ID 0
                { 1, "#00FF00" }, // Green for page ID 1
                { 2, "#0000FF" }  // Blue for page ID 2
            };

            // Watermark settings
            string watermarkText = "CONFIDENTIAL";
            string fontName = "Calibri";
            double fontSizePoints = 72;               // 72 points = 1 inch
            double fontSizeInches = fontSizePoints / 72.0;

            // Add a watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center of the page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use the full page size for the watermark shape
                double width = pageWidth;
                double height = pageHeight;

                // Choose the color for this page; fall back to gray if not defined
                string colorHex;
                if (!palette.TryGetValue(page.ID, out colorHex))
                {
                    colorHex = "#808080"; // Default gray
                }

                // Add the watermark text shape
                page.AddText(pinX, pinY, width, height, watermarkText, fontName, colorHex, fontSizeInches);
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
