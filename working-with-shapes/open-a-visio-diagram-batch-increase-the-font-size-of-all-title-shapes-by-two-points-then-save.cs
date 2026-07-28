using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramFontUpdater <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Increment value for 2 points (1 point = 1/72 inch).
            const double pointIncrementInInches = 2.0 / 72.0;

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify title shapes by checking the universal name (NameU).
                    // Adjust the condition if your diagram uses a different naming convention.
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Increase the font size of each character in the shape.
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Size is stored in inches; add the point increment.
                            ch.Size.Value = ch.Size.Value + pointIncrementInInches;
                        }
                    }
                }
            }

            // Save the modified diagram. Using Vsdx as an example output format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }