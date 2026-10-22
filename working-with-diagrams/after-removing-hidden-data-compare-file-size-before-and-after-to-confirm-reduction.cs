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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Record the original file size before any processing.
            long originalSize = new FileInfo(inputPath).Length;

            // Load the diagram inside a using block as required by the API guidelines.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden information (shapes and masters). Pages flag is omitted because it is not defined.
                diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes | RemoveHiddenInfoItem.Masters));

                // Save the cleaned diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Record the cleaned file size after removal.
            long cleanedSize = new FileInfo(outputPath).Length;

            // Output size comparison results.
            Console.WriteLine($"Original file size: {originalSize} bytes");
            Console.WriteLine($"Cleaned file size : {cleanedSize} bytes");

            if (cleanedSize < originalSize)
            {
                Console.WriteLine("Success: The file size was reduced after removing hidden data.");
            }
            else if (cleanedSize == originalSize)
            {
                Console.WriteLine("Notice: The file size is unchanged after removal.");
            }
            else
            {
                Console.WriteLine("Warning: The file size increased after removal (unexpected).");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}