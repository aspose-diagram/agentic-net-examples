using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Error: No diagram file path provided.");
                Console.WriteLine("Usage: DiagramShapeModifier <inputDiagramPath> [outputDiagramPath]");
                return;
            }

            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: The file \"{inputPath}\" does not exist.");
                return;
            }

            // Determine output path
            string outputPath;
            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            {
                outputPath = args[1];
            }
            else
            {
                string directory = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_modified.vsdx");
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);
                Console.WriteLine($"Loaded diagram: \"{inputPath}\"");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Modify dimensions (example: set width to 2 inches, height to 1 inch)
                        shape.XForm.Width.Value = 2.0;
                        shape.XForm.Height.Value = 1.0;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with modified shape dimensions to: \"{outputPath}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while processing the diagram:");
                Console.WriteLine(ex.Message);
            }
        }
    }