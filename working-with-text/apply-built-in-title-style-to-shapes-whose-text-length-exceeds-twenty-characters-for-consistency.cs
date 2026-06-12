using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
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

            // If the style is not found, we simply skip styling
            if (titleStyle != null)
            {
                // Iterate all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.False)
                        {
                            // Retrieve plain text of the shape
                            string plainText = shape.Text.Value.Text;

                            // Apply the Title style when text length exceeds 20 characters
                            if (!string.IsNullOrEmpty(plainText) && plainText.Length > 20)
                            {
                                shape.TextStyle = titleStyle;
                            }
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
