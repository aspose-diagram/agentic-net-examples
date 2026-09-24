using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Specify the folder containing VSDX files.
            // You can pass the folder path as a command‑line argument or edit the default value.
            string folderPath = args.Length > 0 ? args[0] : @"C:\VisioFiles";

            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder does not exist: {folderPath}");
                return;
            }

            // Get all VSDX files in the folder (non‑recursive).
            string[] files = Directory.GetFiles(folderPath, "*.vsdx", SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
                try
                {
                    // Load the Visio diagram.
                    Diagram diagram = new Diagram(filePath);

                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify connector shapes (1‑D shapes).
                            if (shape.OneD)
                            {
                                // Set the connector's jump style to "none" (default).
                                shape.Layout.ConLineJumpStyle.Value = ConLineJumpStyleValue.PageDefault;
                            }
                        }
                    }

                    // Save the modified diagram, overwriting the original file.
                    diagram.Save(filePath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed: {Path.GetFileName(filePath)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }