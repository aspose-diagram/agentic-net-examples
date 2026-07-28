using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PNG file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioOpacityBatch <inputVisioPath> <outputPngPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set fill foreground and background transparency to 20%
                        // Transparency value: 0 = opaque, 100 = fully transparent
                        shape.Fill.FillForegndTrans.Value = 20;
                        shape.Fill.FillBkgndTrans.Value = 20;
                    }
                }

                // Configure PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

                // Save the diagram as PNG
                diagram.Save(outputPath, pngOptions);

                Console.WriteLine($"Diagram saved to PNG at: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }