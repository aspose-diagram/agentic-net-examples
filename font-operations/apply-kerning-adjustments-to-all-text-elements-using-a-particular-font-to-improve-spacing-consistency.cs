using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path, output path, and target font name
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";
        string targetFontName = "Calibri";

        try
        {
            // Configure fallback font before loading the diagram
            FontConfigs.DefaultFontName = targetFontName;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the font ID for the target font within the diagram
            int targetFontId = -1;
            foreach (Font font in diagram.Fonts)
            {
                if (font.Name == targetFontName)
                {
                    targetFontId = font.ID;
                    break;
                }
            }

            // If the font is not present, add it to the diagram's font collection
            if (targetFontId == -1)
            {
                // Create a new Font object, set its name, and add it to the collection
                Font newFont = new Font();
                newFont.Name = targetFontName;
                diagram.Fonts.Add(newFont);
                targetFontId = newFont.ID;
            }

            // Iterate over all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape contains text
                    if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                    {
                        // Iterate over each character formatting entry
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            // Check if the character uses the target font
                            if (shape.Chars[i].Font.Value == targetFontId)
                            {
                                // Placeholder for kerning adjustment – currently no direct kerning API.
                                // Keeping size unchanged; replace with actual kerning logic if supported.
                                shape.Chars[i].Size.Value = shape.Chars[i].Size.Value;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
