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
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through every page, shape, and paragraph to set line spacing.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Each shape can contain multiple paragraphs.
                foreach (Para para in shape.Paras)
                {
                    // Set line spacing to 1.5 lines.
                    para.SpLine.Value = 1.5;
                }
            }
        }

        // Save the modified diagram (preserving the original format).
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
