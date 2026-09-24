using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the plain text of the shape
                    string plainText = shape.Text.Value.ToString();

                    // If the shape contains multiline text, adjust the default tab stop
                    if (plainText.Contains("\n") || plainText.Contains("\r"))
                    {
                        // Set a larger default tab stop (e.g., 0.5 inches) for better readability
                        shape.TextBlock.DefaultTabStop.Value = 0.5;
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
