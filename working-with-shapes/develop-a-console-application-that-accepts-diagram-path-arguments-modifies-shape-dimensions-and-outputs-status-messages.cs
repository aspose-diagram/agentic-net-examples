using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DiagramModifier <inputPath> [outputPath]");
                return;
            }

            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: File not found - {inputPath}");
                return;
            }

            // Determine output path
            string outputPath;
            if (args.Length >= 2)
            {
                outputPath = args[1];
            }
            else
            {
                string directory = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                outputPath = Path.Combine(directory, fileNameWithoutExt + "_modified.vsdx");
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);
                Console.WriteLine($"Loaded diagram: {inputPath}");

                // Iterate through each page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Store original dimensions
                        double originalWidth = shape.XForm.Width.Value;
                        double originalHeight = shape.XForm.Height.Value;

                        // Increase width and height by 1 inch
                        shape.XForm.Width.Value = originalWidth + 1.0;
                        shape.XForm.Height.Value = originalHeight + 1.0;

                        Console.WriteLine($"Shape ID {shape.ID}: {originalWidth}x{originalHeight} -> {shape.XForm.Width.Value}x{shape.XForm.Height.Value}");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }