using System;
using System.IO;
using Aspose.Diagram;

class VsdxPageCountReport
{
    static void Main()
    {
        // Folder containing VSDX files
        string inputFolder = @"C:\VisioFiles";

        // Output CSV file path
        string csvReportPath = @"C:\VisioReport\PageCounts.csv";

        // Ensure the output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(csvReportPath));

        // Get all VSDX files in the input folder (non‑recursive)
        string[] vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx");

        // Open a StreamWriter for the CSV report
        using (var writer = new StreamWriter(csvReportPath))
        {
            // Write CSV header
            writer.WriteLine("FilePath,PageCount");

            // Process each VSDX file
            foreach (string filePath in vsdxFiles)
            {
                // Load the Visio diagram using the provided constructor (load rule)
                using (var diagram = new Diagram(filePath))
                {
                    // Extract the number of pages from the diagram
                    int pageCount = diagram.Pages.Count;

                    // Write the result as a CSV line
                    // Escape commas in the file path if necessary
                    string escapedPath = filePath.Contains(",") ? $"\"{filePath}\"" : filePath;
                    writer.WriteLine($"{escapedPath},{pageCount}");
                }
            }
        }

        Console.WriteLine($"Report generated at: {csvReportPath}");
    }
}
