using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramProcessor <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // ------------------------------------------------------------
        // 1. Create a custom StyleSheet that defines an underline style.
        // ------------------------------------------------------------
        StyleSheet underlineStyle = new StyleSheet();
        // Assign a unique ID (must be > 0).
        underlineStyle.ID = diagram.StyleSheets.Count + 1;

        // Define a character style with underline.
        Aspose.Diagram.Char underlineChar = new Aspose.Diagram.Char();
        underlineChar.IX = 0; // Index of the character run.
        underlineChar.Style.Value = StyleValue.Underline; // Apply underline.

        // Add the character definition to the stylesheet.
        underlineStyle.Chars.Add(underlineChar);

        // Add the stylesheet to the diagram's collection.
        diagram.StyleSheets.Add(underlineStyle);

        // ------------------------------------------------------------
        // 2. Apply the underline stylesheet to all heading shapes.
        //    Here a heading is identified by its universal name containing "Heading".
        // ------------------------------------------------------------
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Simple heuristic: shape name contains "Heading".
                if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Heading"))
                {
                    // Assign the custom stylesheet to the shape's text style.
                    shape.TextStyle = underlineStyle;
                }
            }
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
