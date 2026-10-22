using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Create a memory stream to capture CSV output.
            using (MemoryStream csvStream = new MemoryStream())
            {
                // Save the diagram to the memory stream in CSV format.
                diagram.Save(csvStream, SaveFileFormat.Csv);

                // Reset stream position to the beginning for reading.
                csvStream.Position = 0;

                // Read the CSV data as text.
                using (StreamReader reader = new StreamReader(csvStream))
                {
                    string csvContent = reader.ReadToEnd();

                    // Output the CSV content to the console (or further processing).
                    Console.WriteLine(csvContent);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}