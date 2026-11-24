using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define default file paths (can be overridden by command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Define a maximum allowed file size (e.g., 5 MB)
        const long maxFileSizeBytes = 5 * 1024 * 1024; // 5 MiB

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Save the diagram in VSDX format to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Verify that the saved file exists
            if (!File.Exists(outputPath))
            {
                Console.Error.WriteLine($"Error: Failed to create output file: {outputPath}");
                return;
            }

            // Get the size of the saved file
            long fileSize = new FileInfo(outputPath).Length;

            // Output the file size information
            Console.WriteLine($"Saved VSDX file size: {fileSize} bytes");

            // Check against the defined limit
            if (fileSize > maxFileSizeBytes)
            {
                Console.Error.WriteLine($"Error: File size exceeds the limit of {maxFileSizeBytes} bytes.");
                // Optionally, you could delete the oversized file or take other actions here
            }
            else
            {
                Console.WriteLine("File size is within the acceptable limit.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions and report them
            Console.Error.WriteLine($"Exception occurred: {ex.Message}");
        }
    }
}