using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing VSDX files.
            string folderPath;
            if (args.Length > 0)
            {
                folderPath = args[0];
            }
            else
            {
                Console.Write("Enter the full path to the folder containing VSDX files: ");
                folderPath = Console.ReadLine()?.Trim() ?? string.Empty;
            }

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            // Get all VSDX files in the specified folder.
            string[] vsdxFiles = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);
            if (vsdxFiles.Length == 0)
            {
                Console.WriteLine("No VSDX files found in the specified folder.");
                return;
            }

            foreach (string filePath in vsdxFiles)
            {
                try
                {
                    // Load the diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page and each shape.
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify connector shapes (1‑D shapes).
                            if (shape.OneD)
                            {
                                // Set the connector's jump style to "none" (page default).
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
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