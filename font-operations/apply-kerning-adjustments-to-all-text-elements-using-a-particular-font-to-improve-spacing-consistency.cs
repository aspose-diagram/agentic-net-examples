using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, target font name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <targetFontName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetFontName = args[2];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape contains text
                        if (shape.Text == null || string.IsNullOrEmpty(shape.Text.Value.Text))
                            continue;

                        // Iterate through all character runs in the shape
                        for (int charIndex = 0; charIndex < shape.Chars.Count; charIndex++)
                        {
                            // Retrieve the character run
                            var charRun = shape.Chars[charIndex];

                            // Get the font index used by this character run
                            int fontIdx = charRun.Font.Value;

                            // Validate font index range
                            if (fontIdx < 0 || fontIdx >= diagram.Fonts.Count)
                                continue;

                            // Retrieve the font name from the diagram's font collection
                            Font font = diagram.Fonts[fontIdx];
                            string fontName = font.Name;

                            // Check if this character uses the target font
                            if (string.Equals(fontName, targetFontName, StringComparison.OrdinalIgnoreCase))
                            {
                                // NOTE: Aspose.Diagram does not expose a direct kerning property.
                                // As a closest alternative, we could adjust character spacing by modifying the character size slightly.
                                // Here we demonstrate a placeholder adjustment (e.g., increase size by 1%).
                                // This is not true kerning but serves as a demonstrative substitution.
                                double originalSize = charRun.Size.Value;
                                charRun.Size.Value = originalSize * 1.01; // Slightly increase size to simulate spacing tweak

                                // Log the adjustment for debugging purposes
                                Console.WriteLine($"Adjusted size for character {charIndex} in shape ID {shape.ID} on page {pageIndex + 1} using font '{targetFontName}'.");
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}