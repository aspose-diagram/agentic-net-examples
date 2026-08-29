using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioTitleFontUpdater <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify title shapes by checking the universal name (NameU).
                    // Adjust the condition as needed for your specific naming convention.
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Increase the font size of each character run by 2 points.
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            // Char.Size.Value is in points. Add 2 points.
                            ch.Size.Value = ch.Size.Value + 2;
                        }
                    }
                }
            }

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }