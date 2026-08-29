using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramProcessing <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text of the shape.
                    string shapeText = shape.Text.Value.Text ?? string.Empty;

                    // Check if the shape contains a warning message (case‑insensitive).
                    if (shapeText.IndexOf("warning", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Apply a solid fill pattern.
                        shape.Fill.FillPattern.Value = 1; // Solid fill.

                        // Set a foreground color (optional, here white).
                        shape.Fill.FillForegnd.Value = "#FFFFFF";

                        // Make the background fully transparent.
                        shape.Fill.FillBkgndTrans.Value = 100; // 100 % transparency.
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }