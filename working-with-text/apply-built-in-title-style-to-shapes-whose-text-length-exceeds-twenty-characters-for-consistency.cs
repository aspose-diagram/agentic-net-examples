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

            // Locate the built‑in "Title" style sheet
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
                Console.WriteLine("The 'Title' style sheet was not found in the document.");
                return;
            }

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text of the shape
                    string plainText = shape.Text.Value.Text;

                    // Apply the Title style if text length exceeds 20 characters
                    if (!string.IsNullOrEmpty(plainText) && plainText.Length > 20)
                    {
                        shape.TextStyle = titleStyle;
                        shape.FillStyle = titleStyle;
                        shape.LineStyle = titleStyle;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram processing completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
