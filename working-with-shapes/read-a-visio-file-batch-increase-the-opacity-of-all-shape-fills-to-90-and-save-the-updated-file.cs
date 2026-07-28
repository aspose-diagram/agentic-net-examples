using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect input and output file paths as command‑line arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioOpacityUpdater <inputVisioPath> <outputVisioPath>");
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

                    // Ensure the Fill object exists before accessing its properties.
                    if (shape.Fill != null)
                    {
                        // Set foreground and background fill transparency to 10%,
                        // which corresponds to 90% opacity.
                        shape.Fill.FillForegndTrans.Value = 10; // 10% transparent
                        shape.Fill.FillBkgndTrans.Value = 10; // 10% transparent
                    }
                }
            }

            // Save the updated diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }