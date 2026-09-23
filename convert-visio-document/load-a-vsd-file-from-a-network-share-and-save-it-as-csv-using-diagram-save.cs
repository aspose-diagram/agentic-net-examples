using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSD file on a network share
            string inputPath = @"\\ServerName\ShareName\Folder\Diagram.vsd";

            // Load the VSD file using Aspose.Diagram
            Diagram diagram = new Diagram(inputPath);

            // Path where the CSV file will be saved
            string outputPath = @"C:\Temp\Diagram.csv";

            // Save the diagram as CSV
            diagram.Save(outputPath, SaveFileFormat.Csv);

        }
        catch (System.IO.IOException ex)
        {
            Console.Error.WriteLine($"[IOException] {ex.Message}");
        }
    }
}
