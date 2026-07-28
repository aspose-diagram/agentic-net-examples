using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths for the original and cleaned diagrams
        string inputPath = "input.vsdx";
        string outputPath = "output_cleaned.vsdx";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Capture the original file size for later comparison
        long originalSize = new FileInfo(inputPath).Length;

        try
        {
            // Load the diagram from the input file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden shapes and masters (Pages flag is not available in this enum)
                diagram.RemoveHiddenInformation((int)(RemoveHiddenInfoItem.Shapes |
                                                       RemoveHiddenInfoItem.Masters));

                // Save the cleaned diagram using the same format as the original
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during Aspose operations
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Capture the cleaned file size after saving
        long cleanedSize = new FileInfo(outputPath).Length;

        // Compare sizes and report the result
        if (cleanedSize < originalSize)
        {
            Console.WriteLine($"Size reduced from {originalSize} bytes to {cleanedSize} bytes.");
        }
        else
        {
            Console.WriteLine($"No size reduction. Original size: {originalSize} bytes, Cleaned size: {cleanedSize} bytes.");
        }
    }
}