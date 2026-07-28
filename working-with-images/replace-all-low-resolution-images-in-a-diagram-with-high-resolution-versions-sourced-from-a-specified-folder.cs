using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input diagram path, folder with high‑resolution images, output diagram path
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ImageReplacementExample <inputDiagram> <highResFolder> <outputDiagram>");
                return;
            }

            string diagramPath = args[0];
            string highResFolder = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Supported image extensions for replacement
            string[] extensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes (foreign objects)
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null)
                    {
                        // Use the shape's name (or universal name) as the base file name
                        string baseName = !string.IsNullOrEmpty(shape.Name) ? shape.Name : shape.NameU;

                        if (string.IsNullOrEmpty(baseName))
                            continue; // Cannot determine a file name, skip

                        string highResPath = null;

                        // Search for a matching high‑resolution file in the folder
                        foreach (string ext in extensions)
                        {
                            string candidate = Path.Combine(highResFolder, baseName + ext);
                            if (File.Exists(candidate))
                            {
                                highResPath = candidate;
                                break;
                            }
                        }

                        // If a matching file is found, replace the image data
                        if (highResPath != null)
                        {
                            try
                            {
                                byte[] imageBytes = File.ReadAllBytes(highResPath);
                                shape.ForeignData.Value = imageBytes;
                                Console.WriteLine($"Replaced image for shape '{baseName}' with '{highResPath}'.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Failed to replace image for shape '{baseName}': {ex.Message}");
                            }
                        }
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }