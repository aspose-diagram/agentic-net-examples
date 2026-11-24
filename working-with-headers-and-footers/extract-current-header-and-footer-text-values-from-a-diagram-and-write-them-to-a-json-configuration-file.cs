using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <input diagram path> <output json path>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the global HeaderFooter object.
                var hf = diagram.HeaderFooter;

                // Create an anonymous object containing the header/footer text values.
                var config = new
                {
                    HeaderLeft = hf.HeaderLeft,
                    HeaderCenter = hf.HeaderCenter,
                    HeaderRight = hf.HeaderRight,
                    FooterLeft = hf.FooterLeft,
                    FooterCenter = hf.FooterCenter,
                    FooterRight = hf.FooterRight
                };

                // Serialize the configuration to formatted JSON.
                string json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });

                // Write JSON to the specified output file.
                File.WriteAllText(outputPath, json);

                Console.WriteLine($"Header and footer configuration saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}