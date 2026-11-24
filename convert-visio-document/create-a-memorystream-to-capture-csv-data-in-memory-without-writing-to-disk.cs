using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine the input Visio file path (first argument or a default placeholder).
        string inputPath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // MemoryStream to hold the CSV output in memory.
        using (MemoryStream csvStream = new MemoryStream())
        {
            try
            {
                // Load the Visio diagram from the specified file.
                Diagram diagram = new Diagram(inputPath);

                // Save the diagram directly to the MemoryStream in CSV format.
                // This overload writes the CSV data into the provided stream.
                diagram.Save(csvStream, SaveFileFormat.Csv);
            }
            catch (Exception ex)
            {
                // Capture any Aspose.Diagram related errors.
                Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
                return;
            }

            // Reset the stream position to the beginning before reading.
            csvStream.Seek(0, SeekOrigin.Begin);

            // Read the CSV content from the MemoryStream as a string.
            string csvContent;
            using (StreamReader reader = new StreamReader(csvStream))
            {
                csvContent = reader.ReadToEnd();
            }

            // Output the CSV data to the console (or further processing as needed).
            Console.WriteLine("CSV data captured in memory:");
            Console.WriteLine(csvContent);
        }
    }
}