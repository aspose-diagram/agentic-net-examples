using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output report file path
        string reportPath = "AltTextReport.txt";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare a list to hold report lines
            List<string> reportLines = new List<string>();
            reportLines.Add($"Alt Text Validation Report - {DateTime.Now}");
            reportLines.Add("---------------------------------------------------");

            int missingCount = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Alt text is stored in the Misc.Comment property (Str2Value)
                    string altText = shape.Misc.Comment.Value;

                    // If Alt text is empty or whitespace, record the shape as missing
                    if (string.IsNullOrWhiteSpace(altText))
                    {
                        missingCount++;
                        string line = $"Page: \"{page.Name}\" (ID={page.ID}), Shape ID={shape.ID}, Name=\"{shape.Name}\" - Missing Alt Text";
                        reportLines.Add(line);
                        Console.WriteLine(line);
                    }
                }
            }

            reportLines.Add("---------------------------------------------------");
            reportLines.Add($"Total shapes missing Alt Text: {missingCount}");

            // Write the report to a text file
            File.WriteAllLines(reportPath, reportLines);

            Console.WriteLine($"Report written to: {Path.GetFullPath(reportPath)}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}