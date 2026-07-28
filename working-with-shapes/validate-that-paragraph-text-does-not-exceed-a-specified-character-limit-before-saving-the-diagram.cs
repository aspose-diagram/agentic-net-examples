using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Define input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Define the maximum allowed characters for any paragraph (shape text)
            int maxCharacters = 1000;

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to validate text length
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve plain text from the shape
                    string text = shape.Text.Value.Text;

                    // If the shape contains text and exceeds the limit, raise an error
                    if (!string.IsNullOrEmpty(text) && text.Length > maxCharacters)
                    {
                        throw new Exception(
                            $"Shape ID {shape.ID} on page \"{page.Name}\" exceeds the character limit " +
                            $"({text.Length} > {maxCharacters}).");
                    }
                }
            }

            // Prepare save options (optional: auto‑fit page to drawing content)
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = true;

            // Save the diagram after validation
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
