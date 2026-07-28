using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (VSDX). Change as needed.
            string inputFolder = @"C:\VisioFiles";

            // Verify folder exists.
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Folder not found: {inputFolder}");
                return;
            }

            // Process each .vsdx file in the folder.
            string[] files = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                try
                {
                    Console.WriteLine($"Processing: {Path.GetFileName(filePath)}");

                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape is not deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Set a local line color to break inheritance.
                            // Using black as a default explicit color.
                            shape.Line.LineColor.Value = "#000000";

                            // Optionally, set other line properties to ensure they are local.
                            // Example: solid line pattern.
                            shape.Line.LinePattern.Value = LinePatternValue.Solid;
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }