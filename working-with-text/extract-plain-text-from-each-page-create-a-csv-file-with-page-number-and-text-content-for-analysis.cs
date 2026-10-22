using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare CSV content with a header row.
            StringBuilder csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("PageNumber,TextContent");

            int pageNumber = 1;

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Collect plain text from all shapes on the current page.
                StringBuilder pageTextBuilder = new StringBuilder();

                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve concatenated plain text of the shape.
                    string shapeText = shape.Text?.Value?.ToString();

                    if (!string.IsNullOrEmpty(shapeText))
                    {
                        // Strip any HTML tags that might be present.
                        string cleanText = Regex.Replace(shapeText, "<.*?>", string.Empty);

                        // Append a space separator if needed.
                        if (pageTextBuilder.Length > 0)
                            pageTextBuilder.Append(' ');

                        pageTextBuilder.Append(cleanText);
                    }
                }

                // Escape double quotes for CSV compliance.
                string csvSafeText = pageTextBuilder.ToString().Replace("\"", "\"\"");

                // Write the page number and its aggregated text to the CSV.
                csvBuilder.AppendLine($"{pageNumber},\"{csvSafeText}\"");

                pageNumber++;
            }

            // Write the CSV data to the specified output file.
            File.WriteAllText(outputPath, csvBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"CSV file successfully created at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}