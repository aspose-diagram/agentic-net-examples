using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output Visio file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramTextCompact <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram
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
                string originalText = shape.Text.Value.Text;

                // If there is no text, continue
                if (string.IsNullOrWhiteSpace(originalText))
                    continue;

                // Replace multiple consecutive spaces with a single space
                string compactText = originalText;
                while (compactText.Contains("  "))
                {
                    compactText = compactText.Replace("  ", " ");
                }

                // If text changed, update the shape's text
                if (!compactText.Equals(originalText))
                {
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(compactText));
                }
            }
        }

        // Save the modified diagram
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
