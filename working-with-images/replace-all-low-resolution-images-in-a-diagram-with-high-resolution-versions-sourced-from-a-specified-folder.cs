using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to the Visio diagram and path to the folder containing high‑resolution images.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <highResImagesFolder>");
            return;
        }

        string diagramPath = args[0];
        string highResFolder = args[1];

        // Guard: verify diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: verify high‑resolution images folder exists.
        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution images folder not found: {highResFolder}");
            return;
        }

        // Build a lookup dictionary of high‑resolution image files (key = file name without extension).
        var highResMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string filePath in Directory.GetFiles(highResFolder))
        {
            string key = Path.GetFileNameWithoutExtension(filePath);
            if (!highResMap.ContainsKey(key))
                highResMap[key] = filePath;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes by checking for foreign (image) type.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Use the shape's NameU (or Name) to locate a matching high‑resolution file.
                            string shapeName = shape.NameU ?? string.Empty;
                            if (string.IsNullOrEmpty(shapeName))
                                continue;

                            string key = Path.GetFileNameWithoutExtension(shapeName);
                            if (highResMap.TryGetValue(key, out string highResPath))
                            {
                                // Read the high‑resolution image bytes.
                                byte[] imageBytes = File.ReadAllBytes(highResPath);

                                // Replace the shape's foreign data with the new image bytes.
                                shape.ForeignData.Value = imageBytes;

                                // Ensure the image stays behind other content.
                                shape.SendToBack();
                            }
                        }
                    }
                }

                // Save the updated diagram to a new file.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(diagramPath) + "_updated.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}