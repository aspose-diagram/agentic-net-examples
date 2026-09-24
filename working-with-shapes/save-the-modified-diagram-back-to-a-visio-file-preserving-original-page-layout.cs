using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file path (use first argument if provided, otherwise default).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output Visio file path (use second argument if provided, otherwise default).
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        // Ensure the directory for the output file exists; create it if necessary.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the diagram from the input file.
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // PLACE FOR MODIFICATIONS:
            // Any changes to pages, shapes, styles, etc., can be performed here.
            // The example does not modify the diagram to keep the original layout.
            // -----------------------------------------------------------------

            // Save the diagram back to a Visio file, preserving the original page layout.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}