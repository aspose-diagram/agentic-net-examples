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
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // ------------------------------------------------------------
                // 1. Create a custom StyleSheet that defines an underline style
                // ------------------------------------------------------------
                StyleSheet underlineStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet
                underlineStyle.ID = diagram.StyleSheets.Count + 1;

                // Define a character style with underline
                Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
                underlineChar.IX = 0; // character index
                underlineChar.Style.Value = StyleValue.Underline; // apply underline

                // Add the character definition to the stylesheet
                underlineStyle.Chars.Add(underlineChar);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(underlineStyle);

                // ------------------------------------------------------------
                // 2. Apply the underline stylesheet to all heading shapes
                //    (Assuming headings contain the word "Heading" in their text)
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Simple heuristic: treat shapes with "Heading" in text as headings
                        if (!string.IsNullOrWhiteSpace(shapeText) && shapeText.Contains("Heading"))
                        {
                            // Assign the custom underline style to the shape's text
                            shape.TextStyle = underlineStyle;
                        }
                    }
                }

                // ------------------------------------------------------------
                // 3. Save the modified diagram
                // ------------------------------------------------------------
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Optional: clean up
                diagram.Dispose();

                Console.WriteLine("Diagram processed and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }