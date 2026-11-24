using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Open the Visio file as a stream.
            using (FileStream stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                // Load the diagram from the stream.
                // Note: Aspose.Diagram does not support password‑protected Visio files via API.
                // The file is loaded without password handling.
                Diagram diagram = new Diagram(stream);

                // Convert the diagram to PDF format.
                diagram.Save(outputPath, SaveFileFormat.Pdf);
            }

            Console.WriteLine($"Conversion successful. PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}