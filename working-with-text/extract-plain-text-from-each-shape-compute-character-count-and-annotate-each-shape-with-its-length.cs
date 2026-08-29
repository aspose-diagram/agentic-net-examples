using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages and shapes.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted.
                if (shape.Del == BOOL.True)
                    continue;

                // Extract plain text from the shape.
                string plainText = shape.Text.Value.Text ?? string.Empty;

                // Compute character count.
                int charCount = plainText.Length;

                // Append the length annotation to the existing text.
                // Example: "Original text (12)"
                shape.Text.Value.Add(new Txt($" ({charCount})"));
            }
        }

        // Save the modified diagram.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
