using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder and output CSV file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input_folder_path> <output_csv_path>");
            return;
        }

        string inputFolder = args[0];
        string outputCsv = args[1];

        // Verify the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Prepare a list to hold file path and page count pairs.
        var results = new List<(string FilePath, int PageCount)>();

        // Get all .vsdx files in the specified folder (non‑recursive).
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

        foreach (string filePath in files)
        {
            // Guard against missing files (should not happen after GetFiles, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Retrieve the number of pages.
                int pageCount = diagram.Pages.Count;

                // Store the result.
                results.Add((filePath, pageCount));
            }
            catch (Exception ex)
            {
                // Report any errors that occur while processing this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        try
        {
            // Write results to the CSV report.
            using (var writer = new StreamWriter(outputCsv, false))
            {
                // CSV header.
                writer.WriteLine("FilePath,PageCount");

                // Write each result line.
                foreach (var entry in results)
                {
                    // Escape double quotes in the file path, if any.
                    string escapedPath = entry.FilePath.Replace("\"", "\"\"");
                    writer.WriteLine($"\"{escapedPath}\",{entry.PageCount}");
                }
            }

            Console.WriteLine($"Report generated successfully at: {outputCsv}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur while writing the CSV file.
            Console.Error.WriteLine($"Failed to write CSV report: {ex.Message}");
        }
    }
}