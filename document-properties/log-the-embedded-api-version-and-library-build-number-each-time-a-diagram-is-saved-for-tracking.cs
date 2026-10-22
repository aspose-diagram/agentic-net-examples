using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Log the embedded API version
            Console.WriteLine($"Diagram API Version: {diagram.Version}");

            // Log the library build numbers (created and edited)
            Console.WriteLine($"Build Number Created: {diagram.DocumentProps.BuildNumberCreated}");
            Console.WriteLine($"Build Number Edited (before save): {diagram.DocumentProps.BuildNumberEdited}");

            // Save the diagram to the specified output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // After saving, log the edited build number again to track changes
            Console.WriteLine($"Build Number Edited (after save): {diagram.DocumentProps.BuildNumberEdited}");

            // Clean up resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}