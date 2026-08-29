using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect: input file, output file, maximum characters per paragraph
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <inputPath> <outputPath> <maxChars>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        if (!int.TryParse(args[2], out int maxChars))
        {
            Console.WriteLine("Invalid maxChars value.");
            return;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Validate paragraph text length before saving
        ValidateParagraphLengths(diagram, maxChars);

        // Save the diagram (VSDX format)
        diagram.Save(outputPath, SaveFileFormat.Vsdx);

        Console.WriteLine("Diagram saved successfully.");
    }

    // Checks each shape's concatenated text; throws if any exceeds the limit
    static void ValidateParagraphLengths(Diagram diagram, int maxChars)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Retrieve plain text of the shape
                string text = shape.Text.Value.Text;

                if (!string.IsNullOrEmpty(text) && text.Length > maxChars)
                {
                    throw new Exception(
                        $"Shape ID {shape.ID} on page '{page.Name}' exceeds the character limit of {maxChars}. Length: {text.Length}");
                }
            }
        }
    }
}
