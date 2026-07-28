using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Emphasis" style sheet (if it exists)
            StyleSheet emphasisStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Emphasis")
                {
                    emphasisStyle = ss;
                    break;
                }
            }

            // If the style is not found, no styling will be applied
            if (emphasisStyle != null)
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the plain text of the shape
                        string shapeText = shape.Text.Value.Text ?? string.Empty;

                        // Apply the Emphasis style to shapes containing the word "Alert"
                        if (shapeText.IndexOf("Alert", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            shape.TextStyle = emphasisStyle;
                            shape.FillStyle = emphasisStyle;
                            shape.LineStyle = emphasisStyle;
                        }
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
