using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define a simple palette mapping names to hex colors
        var palette = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Red", "#FF0000" },
            { "Green", "#00FF00" },
            { "Blue", "#0000FF" },
            { "Gray", "#A5A5A5" }
        };

        // Choose a color key (could be obtained from user input; here we use "Gray")
        string chosenKey = "Gray";

        // Validate palette entry
        if (!palette.TryGetValue(chosenKey, out string fontColor))
        {
            Console.WriteLine($"Palette does not contain key '{chosenKey}'. Using default color.");
            fontColor = "#000000"; // fallback to black
        }

        // Create a new diagram
        Diagram diagram = new Diagram();

        // Get the first page (a new diagram always contains one page)
        Page page = diagram.Pages[0];

        // Retrieve page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Define watermark text and appearance
        string watermarkText = "CONFIDENTIAL";
        string fontName = "Calibri";
        double fontSizePoints = 72; // 1 inch
        double fontSizeInches = fontSizePoints / 72.0;

        // Position watermark at page center
        double pinX = pageWidth / 2.0;
        double pinY = pageHeight / 2.0;

        // Use full page dimensions for the text box so the text is centered
        double width = pageWidth;
        double height = pageHeight;

        // Add the watermark text shape with the selected color
        Shape watermarkShape = page.AddText(pinX, pinY, width, height, watermarkText, fontName, fontColor, fontSizeInches);

        // Optionally rotate the watermark (e.g., 45 degrees)
        double rotationDegrees = 45;
        double rotationRadians = (Math.PI / 180.0) * rotationDegrees;
        watermarkShape.SetAngle(rotationRadians); // SetAngle expects radians

        // Save the diagram
        string outputPath = "WatermarkedDiagram.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine($"Diagram saved to '{outputPath}' with watermark color {fontColor}.");
    }
}
