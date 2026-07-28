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

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Process each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.Text.Value.Text;

                    // If the text is longer than 100 characters, truncate it
                    if (!string.IsNullOrEmpty(text) && text.Length > 100)
                    {
                        string truncated = text.Substring(0, 100) + "...";

                        // Replace the shape's text with the truncated version
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(truncated));
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
