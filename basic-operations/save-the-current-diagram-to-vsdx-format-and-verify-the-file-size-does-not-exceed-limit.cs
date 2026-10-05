using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputVsdxPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Define a maximum allowed file size (e.g., 5 MB).
        const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MiB

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Report any errors that occur during load or save.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Verify that the saved file exists.
        if (!File.Exists(outputPath))
        {
            Console.Error.WriteLine($"Failed to create output file: {outputPath}");
            return;
        }

        // Check the file size against the defined limit.
        long fileSize = new FileInfo(outputPath).Length;
        if (fileSize > MaxFileSizeBytes)
        {
            Console.Error.WriteLine($"Output file size ({fileSize} bytes) exceeds the limit of {MaxFileSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'. File size: {fileSize} bytes.");
        }
    }
}