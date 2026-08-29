using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the built‑in "Emphasis" style sheet
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
                Console.WriteLine("Emphasis style not found in the document.");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    // Apply the Emphasis style to shapes containing the word "Alert"
                    if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Alert"))
                    {
                        shape.TextStyle = emphasisStyle;
                        shape.FillStyle = emphasisStyle;
                        shape.LineStyle = emphasisStyle;
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
