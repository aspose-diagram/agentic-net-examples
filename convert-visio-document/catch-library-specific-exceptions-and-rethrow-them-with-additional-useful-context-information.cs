using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
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
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure image save options for PNG format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.PageIndex = 0; // Export first page only

            // Save the diagram as an image using the configured options
            diagram.Save(outputPath, saveOptions);
            Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
        }
        catch (DiagramException ex) // Catch Aspose.Diagram specific exceptions
        {
            // Add contextual information and rethrow as a generic exception
            string message = $"Failed to process diagram file '{inputPath}'. See inner exception for details.";
            throw new Exception(message, ex);
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            // Add contextual information and rethrow
            string message = $"An unexpected error occurred while handling the diagram: {ex.Message}";
            throw new Exception(message, ex);
        }
    }
}