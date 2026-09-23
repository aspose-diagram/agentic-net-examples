using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output directory for extracted OLE objects
                string outputDir = "OleObjects";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Check if the shape is a foreign OLE object
                        if (shape.Type == TypeValue.Foreign &&
                            shape.ForeignData != null &&
                            shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Get the binary data of the OLE object
                            byte[] oleData = shape.ForeignData.ObjectData;

                            // Ensure there is data to write
                            if (oleData != null && oleData.Length > 0)
                            {
                                // Determine original file extension from the source name
                                string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                                string extension = Path.GetExtension(sourceName);
                                if (string.IsNullOrEmpty(extension))
                                {
                                    extension = ".bin";
                                }

                                // Build a unique file name using the shape ID
                                string fileName = $"Ole_{shape.ID}{extension}";
                                string outputPath = Path.Combine(outputDir, fileName);

                                // Write the binary data to the file
                                File.WriteAllBytes(outputPath, oleData);

                                Console.WriteLine($"Saved OLE object from shape ID {shape.ID} to {outputPath}");
                            }
                        }
                    }
                }

                Console.WriteLine("OLE extraction completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }