using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the folder containing the custom TrueType font
        string fontFolderPath = @"C:\CustomFonts";

        // Path for the output Visio file
        string outputPath = "output.vsdx";

        // Guard: ensure the font folder exists
        if (!Directory.Exists(fontFolderPath))
        {
            Console.Error.WriteLine($"Font folder not found: {fontFolderPath}");
            return;
        }

        // Guard: ensure the output directory exists (if a directory is specified)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory not found: {outputDir}");
            return;
        }

        try
        {
            // Configure Aspose.Diagram to use the custom font folder (recursive search enabled)
            FontConfigs.SetFontFolder(fontFolderPath, true);

            // Set the default font name to the custom font (replace with the actual font name)
            string customFontName = "MyCustomFont";
            FontConfigs.DefaultFontName = customFontName;

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Define rectangle position (center) in inches
            double pinX = 5.0; // X coordinate of the rectangle center
            double pinY = 5.0; // Y coordinate of the rectangle center
            string masterName = "Rectangle";

            // Add a rectangle shape to the page; returns the shape ID
            long rectShapeId = page.AddShape(pinX, pinY, masterName);

            // Retrieve the shape object using its ID
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // Insert multiline text into the rectangle (newline creates separate lines)
            string multilineText = "First line\nSecond line\nThird line";
            rectShape.Text.Value.Add(new Txt(multilineText));

            // Apply character formatting to the inserted text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            rectShape.Chars.Add(ch);
            ch.IX = 0; // Target the first character (applies to the whole text run)
            ch.FontName.Value = customFontName; // Use the custom TrueType font
            ch.Size.Value = 14.0 / 72.0; // Convert 14 points to inches (1 pt = 1/72 inch)
            ch.Color.Value = "#000000"; // Black color (hex string)

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during Aspose operations
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}