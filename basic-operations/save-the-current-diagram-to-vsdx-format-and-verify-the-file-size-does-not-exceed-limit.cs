using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output file paths.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputVsdxPath> [maxSizeInBytes]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Optional size limit argument; default to 5 MB.
        long maxSizeBytes = 5 * 1024 * 1024;
        if (args.Length >= 3 && long.TryParse(args[2], out long parsedLimit) && parsedLimit > 0)
        {
            maxSizeBytes = parsedLimit;
        }

        try
        {
            // Load the diagram from the specified input file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the output directory exists.
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the diagram to VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
            return;
        }

        // Verify the saved file size does not exceed the specified limit.
        try
        {
            FileInfo info = new FileInfo(outputPath);
            long fileSize = info.Length;

            if (fileSize > maxSizeBytes)
            {
                Console.Error.WriteLine($"Saved file size ({fileSize} bytes) exceeds the limit of {maxSizeBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"File saved successfully: {outputPath}");
                Console.WriteLine($"File size: {fileSize} bytes (within limit).");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error checking file size: {ex.Message}");
        }
    }
}