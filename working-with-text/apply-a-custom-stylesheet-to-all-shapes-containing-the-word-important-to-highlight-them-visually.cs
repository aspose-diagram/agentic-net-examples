using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: check if the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (same folder with suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_highlighted.vsdx");

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom StyleSheet to highlight shapes
                // ------------------------------------------------------------
                StyleSheet highlightStyle = new StyleSheet();

                // Assign a unique ID (next available)
                highlightStyle.ID = diagram.StyleSheets.Count + 1;

                // Configure text color (blue)
                Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
                textChar.Color.Value = "#0000FF"; // Blue text
                textChar.IX = 0;
                highlightStyle.Chars.Add(textChar);

                // Configure line color (red)
                highlightStyle.Line.LineColor.Value = "#FF0000"; // Red border

                // Configure fill color (yellow)
                highlightStyle.Fill.FillForegnd.Value = "#FFFF00"; // Yellow fill

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(highlightStyle);

                // ------------------------------------------------------------
                // Iterate through all pages and shapes to apply the style
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the shape's plain text
                        string shapeText = shape.Text.Value.Text ?? string.Empty;

                        // Check if the text contains the keyword "Important"
                        if (shapeText.Contains("Important"))
                        {
                            // Apply the custom stylesheet to text, fill, and line
                            shape.TextStyle = highlightStyle;
                            shape.FillStyle = highlightStyle;
                            shape.LineStyle = highlightStyle;
                        }
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to a new file
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}