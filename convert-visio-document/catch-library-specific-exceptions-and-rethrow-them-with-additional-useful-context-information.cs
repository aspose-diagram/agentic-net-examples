using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.png";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Set up image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram as an image using the configured options
            diagram.Save(outputPath, saveOptions);
        }
        catch (Exception ex) // Catch any exception thrown by Aspose.Diagram operations
        {
            // Rethrow with additional context while preserving the original exception as inner
            throw new Exception($"Failed to process diagram file '{inputPath}'. See inner exception for details.", ex);
        }
    }
}