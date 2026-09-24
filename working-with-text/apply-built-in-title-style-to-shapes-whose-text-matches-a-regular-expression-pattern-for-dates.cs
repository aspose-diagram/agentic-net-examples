using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Find the built‑in "Title" style sheet
            StyleSheet titleStyle = null;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == "Title")
                {
                    titleStyle = ss;
                    break;
                }
            }

            if (titleStyle == null)
            {
                Console.WriteLine("Title style not found in the document.");
                return;
            }

            // Regular expression to match dates (e.g., 12/31/2023)
            Regex dateRegex = new Regex(@"\b\d{2}/\d{2}/\d{4}\b", RegexOptions.Compiled);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    // Apply the Title style if the text matches the date pattern
                    if (!string.IsNullOrEmpty(shapeText) && dateRegex.IsMatch(shapeText))
                    {
                        shape.TextStyle = titleStyle;
                        shape.FillStyle = titleStyle;
                        shape.LineStyle = titleStyle;
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
