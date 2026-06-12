using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    // Maximum allowed characters for any shape's paragraph text
    const int MaxParagraphLength = 1000;

    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate paragraph text length on each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the concatenated plain text of the shape
                    string text = shape.Text.Value.Text ?? string.Empty;

                    // If the text exceeds the limit, raise an exception
                    if (text.Length > MaxParagraphLength)
                    {
                        throw new Exception(
                            $"Shape ID {shape.ID} on page '{page.Name}' exceeds the maximum allowed length of {MaxParagraphLength} characters.");
                    }
                }
            }

            // Save the diagram after successful validation
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
