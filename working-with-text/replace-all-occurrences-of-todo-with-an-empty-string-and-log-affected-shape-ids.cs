using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain text of the shape
                    string text = shape.Text.Value.Text;

                    // If the text contains "TODO", replace it and log the shape ID
                    if (!string.IsNullOrEmpty(text) && text.Contains("TODO"))
                    {
                        shape.ReplaceText("TODO", "");
                        Console.WriteLine($"Shape ID {shape.ID} had TODO removed.");
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
