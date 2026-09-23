using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.png";

        // Verify that the input Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure PNG export options (export first page)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            saveOptions.PageIndex = 0;

            // Save the diagram as a PNG image using the configured options
            diagram.Save(outputPath, saveOptions);
        }
        // Catch Aspose.Diagram-specific exceptions and add context
        catch (DiagramException ex)
        {
            throw new Exception($"Error processing Visio file '{inputPath}'.", ex);
        }
        // Catch any other unexpected exceptions and add context
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error while handling diagram '{inputPath}'.", ex);
        }
    }
}