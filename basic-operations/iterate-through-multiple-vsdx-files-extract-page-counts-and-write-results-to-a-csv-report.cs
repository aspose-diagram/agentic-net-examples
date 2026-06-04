using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class VsdxPageCountReport
{
    static void Main(string[] args)
    {
        // Input folder containing VSDX files
        string inputFolder = @"C:\VisioFiles";

        // Output CSV file path
        string csvReportPath = @"C:\VisioReport\PageCounts.csv";

        // Ensure the output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(csvReportPath));

        // Get all VSDX files in the input folder (non‑recursive)
        var vsdxFiles = Directory.GetFiles(inputFolder, "*.vsdx");

        // Prepare the CSV header
        using (var writer = new StreamWriter(csvReportPath, false))
        {
            writer.WriteLine("FileName,PageCount");

            // Process each VSDX file
            foreach (var filePath in vsdxFiles)
            {
                // Load the diagram using the Diagram(string) constructor
                using (var diagram = new Diagram(filePath))
                {
                    // Retrieve the number of pages in the diagram
                    int pageCount = diagram.Pages.Count;

                    // Write the result to the CSV (file name without path)
                    string fileName = Path.GetFileName(filePath);
                    writer.WriteLine($"{fileName},{pageCount}");
                }
            }
        }

        Console.WriteLine($"Report generated at: {csvReportPath}");
    }
}
