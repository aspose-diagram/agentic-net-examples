using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - Input folder containing VSDX files
            // args[1] - Output folder for modified VSDX files
            // args[2] - Name of the placeholder shape to replace (exact match on Shape.Name)
            // args[3] - Full path to the source PNG image that will replace the placeholder

            if (args.Length != 4)
            {
                Console.WriteLine("Usage: BatchImageReplace <inputFolder> <outputFolder> <placeholderShapeName> <pngPath>");
                return;
            }

            string inputFolder = args[0];
            string outputFolder = args[1];
            string placeholderName = args[2];
            string pngPath = args[3];

            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            if (!File.Exists(pngPath))
            {
                Console.WriteLine($"PNG file not found: {pngPath}");
                return;
            }

            // Ensure output folder exists
            Directory.CreateDirectory(outputFolder);

            // Load PNG bytes once for reuse
            byte[] newImageData = File.ReadAllBytes(pngPath);

            // Process each VSDX file in the input folder
            string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
            foreach (string filePath in vsdxFiles)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify foreign (image) shapes with the specified placeholder name
                            if (shape.Type == TypeValue.Foreign && shape.Name == placeholderName)
                            {
                                // Replace the embedded image data
                                shape.ForeignData.Value = newImageData;
                            }
                        }
                    }

                    // Save the modified diagram to the output folder
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    // Clean up
                    diagram.Dispose();

                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch replacement completed.");
        }
    }