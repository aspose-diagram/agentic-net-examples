using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Find the built‑in "Emphasis" style sheet if it exists
            StyleSheet emphasisStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Emphasis")
                {
                    emphasisStyle = ss;
                    break;
                }
            }

            if (emphasisStyle == null)
            {
                Console.WriteLine("Emphasis style not found in the document. No changes will be applied.");
            }
            else
            {
                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Apply the Emphasis style if the text contains "Alert"
                        if (!string.IsNullOrEmpty(shapeText) &&
                            shapeText.IndexOf("Alert", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            shape.TextStyle = emphasisStyle;
                            shape.FillStyle = emphasisStyle;
                            shape.LineStyle = emphasisStyle;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
