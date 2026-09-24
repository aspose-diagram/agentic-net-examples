using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioOpacityUpdater <inputFilePath> <outputFilePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Desired opacity is 90% -> transparency is 10%.
            double transparencyValue = 10.0;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set foreground fill transparency.
                    shape.Fill.FillForegndTrans.Value = transparencyValue;

                    // Set background fill transparency (if applicable).
                    shape.Fill.FillBkgndTrans.Value = transparencyValue;
                }
            }

            // Save the updated diagram (preserving original format, e.g., VSDX).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with updated fill opacity to '{outputPath}'.");
        }
    }