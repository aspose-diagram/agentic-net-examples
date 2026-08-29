using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new stylesheet that will be applied to matching shapes
            StyleSheet highlightStyle = new StyleSheet();
            // Assign a unique ID for the stylesheet
            highlightStyle.ID = diagram.StyleSheets.Count + 1;

            // Example visual settings: red fill, white text, thick black line
            // Configure character (text) formatting
            Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
            textChar.IX = 0; // first character run
            textChar.Color.Value = "#FFFFFF"; // white text
            textChar.Size.Value = 0.15; // approx 10pt (in inches)
            highlightStyle.Chars.Add(textChar);

            // Configure line formatting (color and weight)
            highlightStyle.Line.LineColor.Value = "#FF0000"; // red line
            highlightStyle.Line.LineWeight.Value = 0.03; // thicker line

            // Configure fill formatting (solid red fill)
            highlightStyle.Fill.FillForegnd.Value = "#FF0000"; // red fill
            // Note: FillPattern can be omitted; default is solid

            // Add the stylesheet to the diagram's collection
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

            // Save the modified diagram using a valid overload (second argument is a SaveFileFormat)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}