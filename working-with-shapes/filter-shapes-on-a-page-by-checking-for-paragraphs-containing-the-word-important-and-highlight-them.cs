using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output_highlighted.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the plain text of the shape
                    string shapeText = shape.Text.Value.Text;

                    // Check if the text contains the word "Important"
                    if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Important"))
                    {
                        // Highlight the shape by setting a yellow fill color
                        shape.Fill.FillForegnd.Value = "#FFFF00";

                        // Optionally, set a contrasting line color
                        shape.Line.LineColor.Value = "#FF0000";
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
