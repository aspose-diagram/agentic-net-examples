using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Set the global default font name (used by the theme)
            // Adjust the font name as needed for your environment
            FontConfigs.DefaultFontName = "Arial";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Apply a preset theme to each page to simulate a global theme
            // (Using Bubble theme as an example)
            foreach (Page page in diagram.Pages)
            {
                // PresetThemeValue is part of Aspose.Diagram namespace
                page.PresetTheme = PresetThemeValue.Bubble;
            }

            // Validate each shape's first paragraph uses the default font
            bool allValid = true;

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has at least one paragraph
                    if (shape.Paras.Count == 0)
                        continue; // No paragraph to validate

                    // Ensure the shape has at least one character (font info)
                    if (shape.Chars.Count == 0)
                        continue; // No character to validate

                    // Retrieve the font name of the first character in the shape
                    // Char.FontName is a StringValue; its .Value holds the font name
                    string firstCharFont = shape.Chars[0].FontName.Value;

                    // Compare with the globally configured default font
                    if (!string.Equals(firstCharFont, FontConfigs.DefaultFontName, StringComparison.OrdinalIgnoreCase))
                    {
                        allValid = false;
                        Console.Error.WriteLine($"Shape ID {shape.ID} on Page {page.ID} does not use the default font. Found: '{firstCharFont}', Expected: '{FontConfigs.DefaultFontName}'.");
                    }
                }
            }

            if (allValid)
            {
                Console.WriteLine("All shapes' first paragraphs use the default font after applying the global theme.");
            }
            else
            {
                Console.Error.WriteLine("Validation completed: some shapes do not use the default font.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}