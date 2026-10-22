using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output directory (optional second argument or default "OleOutputs").
        string outputDir = args.Length >= 2 ? args[1] : "OleOutputs";
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            int oleCounter = 0; // Counter to generate unique file names when needed.

            // Iterate through all pages.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data.
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Attempt to derive a file extension from the source full name.
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        string extension = Path.GetExtension(sourceName);
                        if (string.IsNullOrEmpty(extension))
                        {
                            // Fallback to a generic binary extension if none is found.
                            extension = ".bin";
                        }

                        // Build a unique file name using shape ID and counter to avoid collisions.
                        string fileName = $"Ole_{shape.ID}_{oleCounter}{extension}";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the binary OLE data to the file.
                        File.WriteAllBytes(outputPath, oleData);
                        Console.WriteLine($"Extracted OLE object from Shape ID {shape.ID} to '{outputPath}'.");

                        oleCounter++;
                    }
                }
            }

            Console.WriteLine("OLE extraction completed.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}