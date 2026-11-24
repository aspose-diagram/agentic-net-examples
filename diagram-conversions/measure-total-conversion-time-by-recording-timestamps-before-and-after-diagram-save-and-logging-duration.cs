using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or use defaults.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Record the start timestamp before the conversion.
                DateTime startTime = DateTime.Now;

                // Perform the conversion and save to the desired format.
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                // Record the end timestamp after the conversion.
                DateTime endTime = DateTime.Now;

                // Calculate and log the total conversion duration.
                TimeSpan duration = endTime - startTime;
                Console.WriteLine($"Conversion completed in {duration.TotalMilliseconds} ms.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}