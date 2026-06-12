using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioConnectorStyler <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes) and set their line pattern to dashed.
                    if (shape.OneD)
                    {
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                    }
                }
            }

            // Save the modified diagram. Using VSDX format as an example.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with dashed connectors to: {outputPath}");
        }
    }