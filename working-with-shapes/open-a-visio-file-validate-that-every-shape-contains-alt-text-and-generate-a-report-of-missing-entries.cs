using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: AltTextValidator <inputVisioFile>");
            return;
        }

        string inputPath = args[0];
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var missingEntries = new List<string>();

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Alt text is stored in the Comment cell (Misc.Comment) as Str2Value; retrieve its string value
                    string altText = shape.Misc.Comment.Value;

                    // If Alt text is missing or whitespace, record the shape information
                    if (string.IsNullOrWhiteSpace(altText))
                    {
                        missingEntries.Add(
                            $"Page: {page.NameU}, Shape ID: {shape.ID}, NameU: {shape.NameU}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Output the validation results to the console
        if (missingEntries.Count == 0)
        {
            Console.WriteLine("All shapes contain Alt text.");
        }
        else
        {
            Console.WriteLine("Shapes missing Alt text:");
            foreach (string entry in missingEntries)
            {
                Console.WriteLine(entry);
            }

            // Save the report to a text file
            string reportPath = "AltTextReport.txt";
            File.WriteAllLines(reportPath, missingEntries);
            Console.WriteLine($"Report saved to {reportPath}");
        }
    }
}