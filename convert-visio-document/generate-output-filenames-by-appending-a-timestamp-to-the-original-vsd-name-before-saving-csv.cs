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

            // Load the diagram (creation and loading)
            Diagram diagram = new Diagram(inputVsdPath);

            // Build a timestamp string (e.g., 20230820153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Create the output CSV filename by inserting the timestamp before the extension
            string directory = Path.GetDirectoryName(inputVsdPath);
            string baseName = Path.GetFileNameWithoutExtension(inputVsdPath);
            string outputCsvPath = Path.Combine(directory, $"{baseName}_{timestamp}.csv");

            // Save the diagram as CSV using the provided Save(string, SaveFileFormat) method
            diagram.Save(outputCsvPath, SaveFileFormat.Csv);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
