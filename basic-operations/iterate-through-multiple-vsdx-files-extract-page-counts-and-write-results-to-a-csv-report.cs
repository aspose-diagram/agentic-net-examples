using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class VsdxPageCountReport
{
    static void Main(string[] args)
    {
        // Directory containing VSDX files – adjust as needed
        string inputFolder = @"C:\VisioFiles";

        // Output CSV file path
        string csvReportPath = @"C:\VisioFiles\PageCountReport.csv";

        // Collect results
        var results = new List<(string FileName, int PageCount)>();

        // Iterate over all .vsdx files in the folder
        foreach (string filePath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            // Load the Visio diagram using the Diagram constructor (load rule)
            using (Diagram diagram = new Diagram(filePath))
            {
                // Extract the number of pages
                int pageCount = diagram.Pages.Count;

                // Store the result
                results.Add((Path.GetFileName(filePath), pageCount));
            }
        }

        // Write results to CSV
        using (var writer = new StreamWriter(csvReportPath))
        {
            // Header
            writer.WriteLine("FileName,PageCount");

            // Data rows
            foreach (var entry in results)
            {
                writer.WriteLine($"{entry.FileName},{entry.PageCount}");
            }
        }

        Console.WriteLine($"Report generated at: {csvReportPath}");
    }
}
