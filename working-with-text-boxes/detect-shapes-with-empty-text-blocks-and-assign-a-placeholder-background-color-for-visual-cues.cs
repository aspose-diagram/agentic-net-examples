using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Placeholder background color (yellow) using RGB formula
            const string placeholderColor = "RGB(255,255,0)";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape's text block is empty or whitespace
                    string textContent = shape.Text.Value.ToString();
                    if (string.IsNullOrWhiteSpace(textContent))
                    {
                        // Assign placeholder background color to the text block
                        shape.TextBlock.TextBkgnd.Ufe.F = placeholderColor;
                        // Ensure the background is fully opaque
                        shape.TextBlock.TextBkgndTrans.Value = 0;
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
