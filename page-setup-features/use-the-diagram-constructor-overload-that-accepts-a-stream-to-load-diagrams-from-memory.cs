using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output path for the diagram loaded from memory
        string outputPath = "output_from_memory.vsdx";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the file as a FileStream
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Copy the file stream into a MemoryStream to simulate loading from memory
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    memoryStream.Position = 0; // Reset position to the beginning

                    // Load the diagram from the memory stream using the stream constructor
                    using (Diagram diagram = new Diagram(memoryStream))
                    {
                        // Save the loaded diagram to a new file
                        diagram.Save(outputPath, SaveFileFormat.Vsdx);
                        Console.WriteLine($"Diagram successfully saved to: {outputPath}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}