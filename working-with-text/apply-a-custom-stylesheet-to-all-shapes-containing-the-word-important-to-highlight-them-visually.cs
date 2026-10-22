using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom stylesheet that will be used to highlight shapes.
                // ------------------------------------------------------------
                StyleSheet highlightStyle = new StyleSheet();

                // Assign a unique ID for the stylesheet.
                highlightStyle.ID = diagram.StyleSheets.Count + 1;

                // ----- Character style (text) -----
                // Make the text bold and set its color to red.
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.IX = 0; // Index of the character style.
                charStyle.Color.Value = "#FF0000"; // Red text.
                charStyle.Style.Value = StyleValue.Bold; // Bold text.
                highlightStyle.Chars.Add(charStyle);

                // ----- Line style -----
                // Set line color to red.
                highlightStyle.Line.LineColor.Value = "#FF0000";

                // ----- Fill style -----
                // Set fill foreground color to yellow.
                highlightStyle.Fill.FillForegnd.Value = "#FFFF00";

                // Add the stylesheet to the diagram.
                diagram.StyleSheets.Add(highlightStyle);

                // ------------------------------------------------------------
                // Iterate through all pages and shapes, applying the stylesheet
                // to shapes whose text contains the word "Important".
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's plain text safely.
                        string shapeText = shape.Text?.Value?.Text;

                        // If the shape's text contains "Important", apply the style.
                        if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Important"))
                        {
                            // Apply the custom stylesheet to text, fill, and line.
                            shape.TextStyle = highlightStyle;
                            shape.FillStyle = highlightStyle;
                            shape.LineStyle = highlightStyle;
                        }
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}