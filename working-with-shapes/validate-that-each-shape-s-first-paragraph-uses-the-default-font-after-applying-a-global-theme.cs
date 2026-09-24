using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Set the default font that the theme should use
        FontConfigs.DefaultFontName = "Arial";

        // Input diagram path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a global preset theme to each page
            foreach (Page page in diagram.Pages)
            {
                // PresetTheme is a write‑only property; assign the desired theme
                page.PresetTheme = PresetThemeValue.Bubble;
            }

            // Validate that each shape's first paragraph uses the default font
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without paragraphs or characters
                    if (shape.Paras.Count == 0 || shape.Chars.Count == 0)
                        continue;

                    // Retrieve the first character of the first paragraph
                    Aspose.Diagram.Char firstChar = shape.Chars[0];

                    // Compare the character's font name with the configured default font
                    if (firstChar.FontName.Value != FontConfigs.DefaultFontName)
                    {
                        string message = $"Shape ID {shape.ID} on page '{page.Name}' does not use the default font. " +
                                         $"Found '{firstChar.FontName.Value}', expected '{FontConfigs.DefaultFontName}'.";
                        Console.WriteLine(message);
                        throw new Exception(message);
                    }
                }
            }

            Console.WriteLine("All shapes' first paragraphs use the default font.");

            // Output diagram path
            string outputPath = "output.vsdx";
            // Save the diagram after validation
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}