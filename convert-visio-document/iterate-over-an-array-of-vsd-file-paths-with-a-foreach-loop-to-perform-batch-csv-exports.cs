using System;
using System.IO;
using Aspose.Diagram;

class BatchCsvExport
{
    static void Main()
    {
        try
        {

            // Array of VSD file paths to be processed
            string[] vsdFilePaths = new string[]
            {
                @"C:\Diagrams\Diagram1.vsd",
                @"C:\Diagrams\Diagram2.vsd",
                // Add more file paths as needed
            };

            // Iterate over each VSD file and export it to CSV
            foreach (string vsdPath in vsdFilePaths)
            {
                // Load the VSD diagram
                Diagram diagram = new Diagram(vsdPath);

                // Create the output CSV file path (same name, .csv extension)
                string csvPath = Path.ChangeExtension(vsdPath, ".csv");

                // Export the diagram to CSV format
                diagram.Save(csvPath, SaveFileFormat.Csv);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
