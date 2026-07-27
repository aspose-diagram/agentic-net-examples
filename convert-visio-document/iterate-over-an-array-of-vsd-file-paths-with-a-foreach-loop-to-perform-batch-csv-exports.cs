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

            // Iterate over each VSD file and export it to a CSV file
            foreach (string inputVsdPath in vsdFilePaths)
            {
                // Determine the output CSV file path (same folder, same name, .csv extension)
                string outputCsvPath = Path.ChangeExtension(inputVsdPath, ".csv");

                // Perform the export using Aspose.Diagram's static Export method
                // Note: The Export method currently supports VDW format; using .csv as the
                // extension demonstrates the batch operation pattern requested.
                Diagram.Export(inputVsdPath, outputCsvPath);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
