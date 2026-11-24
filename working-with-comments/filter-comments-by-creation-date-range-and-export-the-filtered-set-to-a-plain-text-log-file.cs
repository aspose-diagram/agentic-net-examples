using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file, output log file, start date, end date
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputLogFile> <startDate> <endDate>");
            Console.Error.WriteLine("Date format: yyyy-MM-dd or any format parsable by DateTime.Parse.");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string startDateStr = args[2];
        string endDateStr = args[3];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse date range
        DateTime startDate;
        DateTime endDate;
        try
        {
            startDate = DateTime.Parse(startDateStr);
            endDate = DateTime.Parse(endDateStr);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error parsing dates: {ex.Message}");
            return;
        }

        // Ensure start <= end
        if (startDate > endDate)
        {
            Console.Error.WriteLine("Start date must be earlier than or equal to end date.");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect filtered comment lines
                List<string> filteredLines = new List<string>();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Access annotations (comments) via PageSheet
                    if (page.PageSheet?.Annotations == null)
                        continue;

                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve creation date; fallback to DateTime.MinValue if null
                        DateTime creationDate = annotation.Date?.Value ?? DateTime.MinValue;

                        // Filter by the specified date range (inclusive)
                        if (creationDate >= startDate && creationDate <= endDate)
                        {
                            // Retrieve comment text safely
                            string commentText = annotation.Comment?.Value ?? string.Empty;

                            // Build a log line with page name (if available), date, and comment
                            string pageName = page.Name ?? $"Page-{page.ID}";
                            string line = $"{pageName}\t{creationDate:yyyy-MM-dd HH:mm:ss}\t{commentText}";
                            filteredLines.Add(line);
                        }
                    }
                }

                // Write filtered comments to the output log file
                try
                {
                    using (StreamWriter writer = new StreamWriter(outputPath, false))
                    {
                        foreach (string line in filteredLines)
                        {
                            writer.WriteLine(line);
                        }
                    }

                    Console.WriteLine($"Filtered comments written to: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error writing to output file: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}