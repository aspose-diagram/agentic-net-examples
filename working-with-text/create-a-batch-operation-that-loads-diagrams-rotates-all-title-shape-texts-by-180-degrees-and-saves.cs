using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Input and output directories can be passed as arguments; otherwise use defaults.
            string inputFolder = args.Length > 0 ? args[0] : "InputDiagrams";
            string outputFolder = args.Length > 1 ? args[1] : "OutputDiagrams";

            // Ensure the output directory exists.
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Process all Visio files (VSDX) in the input folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages and shapes.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify title shapes by their universal name.
                            if (!string.IsNullOrEmpty(shape.NameU) &&
                                shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                            {
                                // Ensure the TextXForm object exists.
                                if (shape.TextXForm != null)
                                {
                                    // Rotate the text by 180 degrees (π radians).
                                    shape.TextXForm.TxtAngle.Value = Math.PI;
                                }
                            }
                        }
                    }

                    // Build the output file path.
                    string outputFileName = Path.GetFileNameWithoutExtension(filePath) + "_rotated.vsdx";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the modified diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }