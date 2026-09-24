using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files to process
            string folderPath = @"C:\VisioFiles";

            // Validate folder existence
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Process each Visio file in the folder (VSDX format)
            string[] files = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
            foreach (string filePath in files)
            {
                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify shapes named "Arrow"
                            if (shape.NameU == "Arrow")
                            {
                                // Rotate shape by adding 90 degrees to its current angle
                                double currentAngle = shape.XForm.Angle.Value;
                                shape.XForm.Angle.Value = currentAngle + 90.0;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }