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
            string inputVsdPath = @"C:\Diagrams\sample.vsd";

            // Generate a timestamp string (e.g., 20231130153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Build the output CSV filename by inserting the timestamp before the extension
            string directory = Path.GetDirectoryName(inputVsdPath);
            string baseName = Path.GetFileNameWithoutExtension(inputVsdPath);
            string outputCsvPath = Path.Combine(directory, $"{baseName}_{timestamp}.csv");

            // Load the VSD diagram (using the Diagram constructor that accepts a file path)
            Diagram diagram = new Diagram(inputVsdPath);

            // Save the diagram as CSV using the generated filename
            diagram.Save(outputCsvPath, SaveFileFormat.Csv);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
