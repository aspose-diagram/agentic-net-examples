using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputDirectory>");
            return;
        }

        string inputPath = args[0];
        string outputDir = args[1];

        // Guard: input file must exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            int oleCounter = 0;

            // Iterate through all pages
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape contains foreign (OLE) data
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        byte[] oleData = shape.ForeignData.ObjectData;

                        // Determine original file extension from ObjectSourceFullName, fallback to .bin
                        string sourceName = shape.ForeignData.ObjectSourceFullName ?? string.Empty;
                        string extension = Path.GetExtension(sourceName);
                        if (string.IsNullOrEmpty(extension))
                        {
                            extension = ".bin";
                        }

                        // Build a unique file name using shape ID and counter
                        string fileName = $"OleObject_{shape.ID}_{oleCounter}{extension}";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Write the binary data to the file
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
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}