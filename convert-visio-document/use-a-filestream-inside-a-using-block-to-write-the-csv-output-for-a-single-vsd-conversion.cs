using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input Visio file path (VSD) and output CSV file path.
        string inputPath = "input.vsd";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.csv";

        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Use a FileStream inside a using block to write the CSV output.
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the diagram as CSV directly to the FileStream.
                diagram.Save(outputStream, SaveFileFormat.Csv);
            }

            Console.WriteLine($"Diagram successfully converted to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or saving to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}