using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source VSD file
            string inputVsd = "sample.vsd";

            // Load the diagram (uses Aspose.Diagram constructor)
            Diagram diagram = new Diagram(inputVsd);

            // Create a timestamp string (e.g., 20231130153045)
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

            // Build the output CSV filename by inserting the timestamp before the extension
            string baseName = Path.GetFileNameWithoutExtension(inputVsd);
            string outputCsv = $"{baseName}_{timestamp}.csv";

            // Save the diagram as CSV using the Save method rule
            diagram.Save(outputCsv, SaveFileFormat.Csv);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
