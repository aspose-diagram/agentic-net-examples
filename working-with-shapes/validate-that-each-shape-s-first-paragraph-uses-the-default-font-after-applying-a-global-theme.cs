using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Apply a preset theme to each page to simulate a global theme.
            // The chosen theme (Bubble) and variant are arbitrary examples.
            foreach (Page page in diagram.Pages)
            {
                // Set the preset theme for the page.
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Retrieve the default font name configured for the diagram.
            // If not set explicitly, Aspose.Diagram uses its internal default.
            string defaultFontName = FontConfigs.DefaultFontName;

            // If the default font name is not set, inform the user.
            if (string.IsNullOrEmpty(defaultFontName))
            {
                Console.WriteLine("Default font name is not set. Validation will use the system default.");
                defaultFontName = "Arial"; // Fallback for validation purposes.
            }

            bool allValid = true;

            // Iterate through all pages and shapes to validate the first paragraph's font.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has at least one paragraph.
                    if (shape.Paras.Count == 0)
                        continue; // No paragraph to validate.

                    // Validate the font of the first character (as a proxy for the paragraph's font).
                    if (shape.Chars.Count > 0)
                    {
                        // Access the first character.
                        Aspose.Diagram.Char firstChar = shape.Chars[0];

                        // The Char class stores the font name in the FontName cell.
                        // Compare it with the default font name.
                        string charFontName = firstChar.FontName.Value;

                        if (!string.Equals(charFontName, defaultFontName, StringComparison.OrdinalIgnoreCase))
                        {
                            allValid = false;
                            Console.WriteLine($"Shape ID {shape.ID} on Page {page.ID} uses font '{charFontName}' instead of default '{defaultFontName}'.");
                        }
                    }
                }
            }

            if (allValid)
            {
                Console.WriteLine("All shapes' first paragraphs use the default font.");
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}