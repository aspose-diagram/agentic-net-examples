using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output CSV file path
        string outputPath = "output.csv";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // NOTE:
            // Aspose.Diagram does not provide an Encoding property on SaveOptions for CSV export.
            // Therefore, the CSV is saved using the library's default encoding (UTF‑8).
            // This is the closest possible implementation to the requested task.

            // Save the diagram as CSV
            diagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine($"Diagram saved to CSV at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}