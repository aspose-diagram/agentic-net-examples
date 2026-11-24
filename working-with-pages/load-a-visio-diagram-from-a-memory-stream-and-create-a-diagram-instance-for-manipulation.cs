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
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Read the Visio file into a memory buffer.
            byte[] fileBytes = File.ReadAllBytes(inputPath);

            // Load the diagram from the memory stream.
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Diagram must be instantiated inside a using block.
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // Example manipulation: output the number of pages.
                    Console.WriteLine($"Diagram loaded successfully. Page count: {diagram.Pages.Count}");

                    // Optional: save the diagram to a new file to demonstrate further processing.
                    string outputPath = Path.Combine(
                        Path.GetDirectoryName(inputPath) ?? string.Empty,
                        "output.vsdx");

                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}