using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Apply a global theme (optional). Example: copy theme from another diagram.
                // string themePath = "theme.vsdx";
                // Diagram themeDiagram = new Diagram(themePath);
                // diagram.CopyTheme(themeDiagram);

                // Retrieve the default font name configured for the diagram
                string defaultFont = FontConfigs.DefaultFontName;

                if (string.IsNullOrEmpty(defaultFont))
                {
                    Console.WriteLine("Default font is not set. Validation cannot be performed.");
                    return;
                }

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has at least one paragraph and one character run
                        if (shape.Paras.Count == 0 || shape.Chars.Count == 0)
                            continue;

                        // Get the font name of the first character (which belongs to the first paragraph)
                        string firstCharFont = shape.Chars[0].FontName.Value;

                        // Validate against the default font
                        if (!string.Equals(firstCharFont, defaultFont, StringComparison.OrdinalIgnoreCase))
                        {
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' uses font '{firstCharFont}' " +
                                             $"instead of the default font '{defaultFont}'.";
                            // Report the mismatch
                            Console.WriteLine(message);
                            // Optionally, throw an exception to halt execution
                            throw new Exception(message);
                        }
                    }
                }

                Console.WriteLine("All shapes' first paragraphs use the default font.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }