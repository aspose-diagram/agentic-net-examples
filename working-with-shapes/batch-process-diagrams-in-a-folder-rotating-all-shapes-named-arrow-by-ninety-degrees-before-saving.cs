using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder to process: argument or current directory
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Process each Visio file in the folder (common extensions)
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                // Simple filter for Visio file extensions
                string ext = Path.GetExtension(filePath).ToLowerInvariant();
                if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx")
                {
                    continue;
                }

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(filePath))
                    {
                        // Iterate through all pages and shapes
                        foreach (Page page in diagram.Pages)
                        {
                            foreach (Shape shape in page.Shapes)
                            {
                                // Check for shapes named "Arrow"
                                if (shape.NameU == "Arrow")
                                {
                                    // Rotate 90 degrees (π/2 radians)
                                    shape.SetAngle(Math.PI / 2);
                                }
                            }
                        }

                        // Save back to the same file, preserving format
                        // Use Vsdx as default; Visio will handle other formats similarly
                        diagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Processed and saved: {Path.GetFileName(filePath)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }
        }
    }