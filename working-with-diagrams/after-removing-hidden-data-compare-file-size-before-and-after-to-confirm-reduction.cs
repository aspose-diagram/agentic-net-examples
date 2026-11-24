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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found -> {inputPath}");
            return;
        }

        // Prepare output path (same folder, suffix "_cleaned").
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_cleaned" + Path.GetExtension(inputPath));

        long originalSize = 0; // will be set after loading diagram

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Record original file size.
                originalSize = new FileInfo(inputPath).Length;

                // Remove hidden information: shapes and masters (Pages flag not available).
                int flags = (int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters);
                diagram.RemoveHiddenInformation(flags);

                // Save the cleaned diagram in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Record cleaned file size.
            long cleanedSize = new FileInfo(outputPath).Length;

            // Compare sizes and report the result.
            if (cleanedSize < originalSize)
            {
                Console.WriteLine($"Success: File size reduced from {originalSize} bytes to {cleanedSize} bytes.");
            }
            else
            {
                Console.WriteLine($"No reduction: Original size {originalSize} bytes, cleaned size {cleanedSize} bytes.");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
