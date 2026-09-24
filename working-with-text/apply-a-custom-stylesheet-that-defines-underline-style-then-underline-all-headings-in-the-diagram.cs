using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a custom stylesheet that defines an underline character style
            StyleSheet underlineStyle = new StyleSheet();
            underlineStyle.ID = diagram.StyleSheets.Count + 1;
            underlineStyle.Name = "UnderlineStyle";

            // Define a character with underline formatting
            Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
            underlineChar.IX = 0; // character index
            underlineChar.Style.Value = StyleValue.Underline; // apply underline
            underlineStyle.Chars.Add(underlineChar);

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(underlineStyle);

            // Iterate through all pages and shapes to apply the stylesheet to headings
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape
                    string shapeText = shape.Text.Value.ToString();

                    // Simple heuristic: treat shapes whose text starts with "Heading" as headings
                    if (!string.IsNullOrWhiteSpace(shapeText) && shapeText.StartsWith("Heading"))
                    {
                        // Apply the underline stylesheet to the shape's text
                        shape.TextStyle = underlineStyle;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
