using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional output path
        string outputPath = args.Length > 1 ? args[1] : null;

        try
        {
            // Load the Visio file into a memory stream
            MemoryStream memoryStream;
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // Reset position for reading
            }

            // Create a Diagram instance from the memory stream
            Diagram diagram = new Diagram(memoryStream);

            // Example manipulation: display the number of pages
            Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

            // If an output path is provided, save the diagram back to a file
            if (!string.IsNullOrEmpty(outputPath))
            {
                // Guard: ensure the directory for the output file exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
                    return;
                }

                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}