using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the folder containing Visio files.
                // If a folder path is passed as an argument, use it; otherwise use the current directory.
                string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

                // Find all Visio files (VSDX format) in the folder.
                string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");

                // Prepare an output subfolder to store the modified files.
                string outputFolder = Path.Combine(inputFolder, "Processed");
                Directory.CreateDirectory(outputFolder);

                // Rotation angle: 30 degrees converted to radians (required by TxtAngle).
                double angleRadians = Math.PI / 180.0 * 30.0;

                foreach (string filePath in visioFiles)
                {
                    // Load the diagram from file.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through every page and every shape.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve plain text of the shape.
                            string shapeText = shape.Text.Value.Text;

                            // Apply rotation only if the shape contains text.
                            if (!string.IsNullOrWhiteSpace(shapeText))
                            {
                                shape.TextXForm.TxtAngle.Value = angleRadians;
                            }
                        }
                    }

                    // Save the modified diagram to the output folder, preserving the original format.
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed. Modified files are located in: " + outputFolder);

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }
    }