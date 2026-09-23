using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputFilePath = @"C:\Diagrams\sample.vsd";

            // Load the VSD diagram
            Diagram diagram = new Diagram(inputFilePath);

            // Create a timestamp string (e.g., 20230921_154530)
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Build the output CSV file name by inserting the timestamp before the extension
            string directory = Path.GetDirectoryName(inputFilePath);
            string baseName = Path.GetFileNameWithoutExtension(inputFilePath);
            string outputFileName = $"{baseName}_{timestamp}.csv";
            string outputFilePath = Path.Combine(directory, outputFileName);

            // Save the diagram as CSV using the generated file name
            diagram.Save(outputFilePath, SaveFileFormat.Csv);

            Console.WriteLine($"Diagram saved as CSV: {outputFilePath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
