using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram, high‑resolution images folder, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: ReplaceImages <inputDiagram> <highResFolder> <outputDiagram>");
            return;
        }

        string inputDiagramPath = args[0];
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"File not found: {inputDiagramPath}");
            return;
        }

        string highResFolder = args[1];
        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"Folder not found: {highResFolder}");
            return;
        }

        string outputDiagramPath = args[2];
        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputDiagramPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputDiagramPath);

            // Pre‑load all high‑resolution image file names for quick lookup
            var highResFiles = Directory.GetFiles(highResFolder);
            // Build a dictionary keyed by file name without extension (case‑insensitive)
            var highResMap = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in highResFiles)
            {
                string key = Path.GetFileNameWithoutExtension(file);
                if (!highResMap.ContainsKey(key))
                    highResMap[key] = file;
            }

            // Iterate through every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes – they are of TypeValue.Foreign
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Use the universal name (NameU) if available; otherwise fallback to Name
                        string shapeName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : shape.Name;
                        if (string.IsNullOrEmpty(shapeName))
                            continue; // Skip shapes without a recognizable name

                        // Attempt to locate a matching high‑resolution file (ignoring extension)
                        if (highResMap.TryGetValue(shapeName, out string highResPath))
                        {
                            // Read the high‑resolution image bytes
                            byte[] imageBytes = File.ReadAllBytes(highResPath);
                            // Replace the foreign data (raw image) with the new bytes
                            shape.ForeignData.Value = imageBytes;
                            Console.WriteLine($"Replaced image for shape '{shapeName}' with '{Path.GetFileName(highResPath)}'.");
                        }
                        else
                        {
                            // No matching high‑resolution file found; leave the original image unchanged
                            Console.WriteLine($"No high‑resolution image found for shape '{shapeName}'.");
                        }
                    }
                }
            }

            // Save the modified diagram to the desired output path (VSDX format)
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputDiagramPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}