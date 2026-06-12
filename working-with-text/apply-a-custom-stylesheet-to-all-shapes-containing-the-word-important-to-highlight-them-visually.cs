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

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new stylesheet that will highlight shapes
            StyleSheet highlightStyle = new StyleSheet();
            // Assign a unique ID (next available in the collection)
            highlightStyle.ID = diagram.StyleSheets.Count + 1;

            // Set fill foreground color (light yellow)
            highlightStyle.Fill.FillForegnd.Value = "#FFFF99";

            // Set line color (red)
            highlightStyle.Line.LineColor.Value = "#FF0000";

            // Set text color (black) via a Char entry
            Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
            textChar.IX = 0; // character index
            textChar.Color.Value = "#000000";
            highlightStyle.Chars.Add(textChar);

            // Add the stylesheet to the diagram's collection
            diagram.StyleSheets.Add(highlightStyle);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape
                    string plainText = shape.Text.Value.Text;

                    // Apply the stylesheet if the text contains the word "Important"
                    if (!string.IsNullOrEmpty(plainText) && plainText.Contains("Important"))
                    {
                        shape.TextStyle = highlightStyle;
                        shape.FillStyle = highlightStyle;
                        shape.LineStyle = highlightStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
