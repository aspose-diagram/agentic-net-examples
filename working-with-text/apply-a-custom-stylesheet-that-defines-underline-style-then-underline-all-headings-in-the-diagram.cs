using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // -------------------------------------------------
                // 1. Create a custom StyleSheet that defines an underline style
                // -------------------------------------------------
                StyleSheet underlineStyle = new StyleSheet();

                // Assign a unique ID (must be greater than existing IDs)
                underlineStyle.ID = diagram.StyleSheets.Count + 1;

                // Optional: give the style a name for readability
                underlineStyle.Name = "UnderlineStyle";

                // Define a character formatting entry with underline
                Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
                underlineChar.IX = 0; // character index within the style
                underlineChar.Style.Value = StyleValue.Underline; // apply underline

                // Add the character definition to the stylesheet
                underlineStyle.Chars.Add(underlineChar);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(underlineStyle);

                // -------------------------------------------------
                // 2. Apply the underline stylesheet to all heading shapes
                //    (assumes headings are identified by the word "Heading" in their name)
                // -------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify heading shapes by name (case‑insensitive)
                        if (!string.IsNullOrEmpty(shape.NameU) &&
                            shape.NameU.IndexOf("Heading", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply the custom underline style to the shape's text
                            shape.TextStyle = underlineStyle;
                        }
                    }
                }

                // -------------------------------------------------
                // 3. Save the modified diagram
                // -------------------------------------------------
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }