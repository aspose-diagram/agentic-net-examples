using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate command line arguments
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: DiagramShapeModifier <inputDiagramPath> [outputDiagramPath]");
                return;
            }

            string inputPath = args[0];
            string outputPath = args.Length >= 2 ? args[1] : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

            // Check if input file exists
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: Input file not found: {inputPath}");
                return;
            }

            try
            {
                Console.WriteLine($"Loading diagram from: {inputPath}");
                Diagram diagram = new Diagram(inputPath);

                // Define new dimensions (in inches)
                double newWidth = 2.0;
                double newHeight = 1.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Update width and height
                        shape.XForm.Width.Value = newWidth;
                        shape.XForm.Height.Value = newHeight;
                    }
                }

                Console.WriteLine($"Saving modified diagram to: {outputPath}");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram processing completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }