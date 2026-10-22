using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as an argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Dictionary to hold shape ID -> list of font names used in that shape.
        Dictionary<long, List<string>> shapeFonts = new Dictionary<long, List<string>>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Use a HashSet to avoid duplicate font names per shape.
                    HashSet<string> fontsInShape = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    // Iterate over character runs to collect font information.
                    for (int c = 0; c < shape.Chars.Count; c++)
                    {
                        var charRun = shape.Chars[c];

                        // The Font property holds an index into the diagram's font collection.
                        int fontIdx = charRun.Font.Value;

                        // Validate the index before accessing the font collection.
                        if (fontIdx >= 0 && fontIdx < diagram.Fonts.Count)
                        {
                            var font = diagram.Fonts[fontIdx];
                            // Font.Name provides the font's display name.
                            string fontName = font.Name;
                            if (!string.IsNullOrEmpty(fontName))
                                fontsInShape.Add(fontName);
                        }
                    }

                    // If any fonts were found, store them in the result dictionary.
                    if (fontsInShape.Count > 0)
                    {
                        shapeFonts[shape.ID] = new List<string>(fontsInShape);
                    }
                }
            }

            // Output the collected font information.
            Console.WriteLine("Shape ID -> Font Names");
            foreach (var kvp in shapeFonts)
            {
                Console.WriteLine($"{kvp.Key} -> {string.Join(", ", kvp.Value)}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}