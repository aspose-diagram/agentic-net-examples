using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: RemoveHiddenInfo <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }
        else
        {
            // Insert "_cleaned" before the file extension
            string dir = Path.GetDirectoryName(inputPath) ?? string.Empty;
            string name = Path.GetFileNameWithoutExtension(inputPath);
            string ext = Path.GetExtension(inputPath);
            outputPath = Path.Combine(dir, $"{name}_cleaned{ext}");
        }

        try
        {
            // Record original file size
            long originalSize = new FileInfo(inputPath).Length;

            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Combine flags to remove hidden shapes, masters, and styles
                int flags = (int)(RemoveHiddenInfoItem.Shapes |
                                  RemoveHiddenInfoItem.Masters |
                                  RemoveHiddenInfoItem.Styles);

                // Remove hidden information
                diagram.RemoveHiddenInformation(flags);

                // Save the cleaned diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Record cleaned file size
            long cleanedSize = new FileInfo(outputPath).Length;

            // Log the amount of data removed
            long bytesRemoved = originalSize - cleanedSize;
            Console.WriteLine($"Original size: {originalSize} bytes");
            Console.WriteLine($"Cleaned size:  {cleanedSize} bytes");
            Console.WriteLine($"Data removed:  {bytesRemoved} bytes");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}