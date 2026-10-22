using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

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
            // Ensure the diagram is properly disposed after use
            using (diagram)
            {
                // Start timing the conversion process
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Determine the desired output format based on the file extension
                string extension = Path.GetExtension(outputPath).ToLowerInvariant();
                if (extension == ".pdf")
                {
                    // Save as PDF
                    diagram.Save(outputPath, SaveFileFormat.Pdf);
                }
                else
                {
                    // Default to VSDX format for other extensions
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                // Stop the timer after saving completes
                stopwatch.Stop();

                // Log the total conversion duration in milliseconds
                Console.WriteLine($"Conversion completed in {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}