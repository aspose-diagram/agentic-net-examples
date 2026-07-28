using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramBatchProcessor <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the current width of the shape (in inches).
                        double width = shape.XForm.Width.Value;

                        // Calculate a new line weight based on the width.
                        // Example: 1% of the width.
                        double newLineWeight = width * 0.01;

                        // Assign the calculated line weight to the shape.
                        shape.Line.LineWeight.Value = newLineWeight;
                    }
                }

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed successfully.");
        }
    }