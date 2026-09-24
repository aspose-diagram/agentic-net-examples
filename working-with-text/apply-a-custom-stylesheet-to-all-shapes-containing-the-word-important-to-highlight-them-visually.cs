using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a custom stylesheet to highlight shapes
            StyleSheet highlightStyle = new StyleSheet();
            highlightStyle.ID = diagram.StyleSheets.Count + 1;

            // Set line color (red) and fill foreground color (yellow)
            highlightStyle.Line.LineColor.Value = "#FF0000";
            highlightStyle.Fill.FillForegnd.Value = "#FFFF00";

            // Optional: set a character style (white text)
            Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
            charStyle.IX = 0;
            charStyle.Color.Value = "#FFFFFF";
            highlightStyle.Chars.Add(charStyle);

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(highlightStyle);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape
                    string shapeText = shape.Text.Value.ToString();

                    // Apply the stylesheet if the text contains the word "Important"
                    if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Important"))
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
