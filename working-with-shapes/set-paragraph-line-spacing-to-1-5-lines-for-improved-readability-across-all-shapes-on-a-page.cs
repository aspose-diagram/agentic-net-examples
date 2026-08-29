using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramParagraphSpacing <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through each paragraph of the shape's text.
                    foreach (Para para in shape.Paras)
                    {
                        // Set line spacing to 1.5 lines.
                        // The SpLine property controls line spacing; value is in points.
                        // A value of 1.5 corresponds to 1.5 lines.
                        para.SpLine.Value = 1.5;
                    }
                }
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }