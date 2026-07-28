using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing diagrams.
            string inputFolder;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                inputFolder = args[0];
            else
                inputFolder = "Diagrams";

            // Verify the folder exists.
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Input folder does not exist: {inputFolder}");
                return;
            }

            // Find all Visio VSDX files in the folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram.
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through each page.
                        foreach (Aspose.Diagram.Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the page.
                            foreach (Aspose.Diagram.Shape shape in page.Shapes)
                            {
                                // Identify title shapes by checking the universal name.
                                if (!string.IsNullOrEmpty(shape.NameU) &&
                                    shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    // Rotate the text by 180 degrees (π radians).
                                    shape.TextXForm.TxtAngle.Value = Math.PI;
                                }
                            }
                        }

                        // Build the output file name.
                        string directory = Path.GetDirectoryName(filePath);
                        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_rotated.vsdx");

                        // Save the modified diagram.
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Processed and saved: {outputPath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }