using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the folder containing VSDX files
                string inputFolder = @"C:\VsdxFiles";

                // Path to the output CSV report
                string csvReportPath = @"C:\VsdxReport\PageCounts.csv";

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(csvReportPath));

                // Collect results: each entry is (FileName, PageCount)
                var results = new List<(string FileName, int PageCount)>();

                // Iterate over all .vsdx files in the input folder
                foreach (string filePath in Directory.EnumerateFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        // Load the VSDX file using Aspose.Diagram
                        Diagram diagram = new Diagram(filePath);

                        // Get the number of pages in the diagram
                        int pageCount = diagram.Pages.Count;

                        // Store the result
                        results.Add((Path.GetFileName(filePath), pageCount));
                    }
                    catch (Exception ex)
                    {
                        // If loading fails, record zero pages and continue
                        Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                        results.Add((Path.GetFileName(filePath), 0));
                    }
                }

                // Write the results to a CSV file
                using (var writer = new StreamWriter(csvReportPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("FileName,PageCount");

                    // Write each result line
                    foreach (var entry in results)
                    {
                        writer.WriteLine($"{EscapeCsv(entry.FileName)},{entry.PageCount}");
                    }
                }

                Console.WriteLine($"Report generated at: {csvReportPath}");

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
            }
    }

        // Helper method to escape CSV fields that may contain commas or quotes
        private static string EscapeCsv(string field)
        {
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                field = field.Replace("\"", "\"\"");
                return $"\"{field}\"";
            }
            return field;
        }
    }