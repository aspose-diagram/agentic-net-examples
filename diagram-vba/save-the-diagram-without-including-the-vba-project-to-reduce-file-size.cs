using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_no_vba.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Remove the VBA project data to reduce the file size
            // Setting VbProjectData to null strips all macro information
            diagram.VbProjectData = null;

            // Save the diagram without VBA using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Dispose the diagram to release any unmanaged resources
            diagram.Dispose();

            Console.WriteLine($"Diagram saved without VBA to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}