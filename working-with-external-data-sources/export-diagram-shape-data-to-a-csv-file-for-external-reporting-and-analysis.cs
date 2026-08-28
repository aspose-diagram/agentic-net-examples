using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <executable> <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Export shape data to CSV format
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"Diagram exported to CSV successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
