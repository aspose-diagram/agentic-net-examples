using System;
using System.IO;
using System.Collections.Generic;
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

        string inputPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Build a lookup dictionary for fonts defined in the diagram
                var diagramFonts = new Dictionary<string, Font>(StringComparer.OrdinalIgnoreCase);
                foreach (Font font in diagram.Fonts)
                {
                    // Font.Name uniquely identifies a font in the diagram
                    if (!string.IsNullOrEmpty(font.Name))
                    {
                        diagramFonts[font.Name] = font;
                    }
                }

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape contains any text
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            continue; // No text to process
                        }

                        // Collect font names and their first encountered style from character runs
                        var fontStyles = new Dictionary<string, StyleValue>(StringComparer.OrdinalIgnoreCase);
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            string fontName = ch.FontName?.Value;
                            if (string.IsNullOrEmpty(fontName))
                                continue;

                            // Record the style if this font hasn't been seen yet for this shape
                            if (!fontStyles.ContainsKey(fontName))
                            {
                                // Style may be undefined; use the cell's value if present
                                StyleValue style = ch.Style?.Value ?? StyleValue.Undefined;
                                fontStyles[fontName] = style;
                            }
                        }

                        // If no character-level font information is found, note it
                        if (fontStyles.Count == 0)
                        {
                            Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shape.Name}");
                            Console.WriteLine("  No character-level font information found.");
                            continue;
                        }

                        // Output font metadata for each font used by this shape
                        foreach (KeyValuePair<string, StyleValue> kvp in fontStyles)
                        {
                            string fontName = kvp.Key;
                            StyleValue style = kvp.Value;

                            Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shape.Name}");
                            Console.WriteLine($"  Font Family: {fontName}");
                            Console.WriteLine($"  Style: {style}");
                            // Aspose.Diagram does not expose a version property for fonts
                            Console.WriteLine($"  Version: N/A");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}