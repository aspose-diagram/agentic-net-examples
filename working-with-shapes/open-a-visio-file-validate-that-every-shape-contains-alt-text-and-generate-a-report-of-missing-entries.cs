using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output report file path
        string reportPath = "AltTextReport.txt";

        // List to hold information about shapes missing Alt text
        List<string> missingAltTextEntries = new List<string>();

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve Alt text from shape.Misc.Comment.Value
                    string altText = null;
                    if (shape.Misc != null && shape.Misc.Comment != null)
                    {
                        altText = shape.Misc.Comment.Value;
                    }

                    // If Alt text is null or whitespace, record the shape
                    if (string.IsNullOrWhiteSpace(altText))
                    {
                        // Use page.Name (since Page.Index does not exist) to identify the page
                        string entry = $"Page Name: {page.Name}, Shape ID: {shape.ID}, NameU: {shape.NameU}";
                        missingAltTextEntries.Add(entry);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Generate report
        using (StreamWriter writer = new StreamWriter(reportPath))
        {
            writer.WriteLine("Visio Alt Text Validation Report");
            writer.WriteLine($"Generated on: {DateTime.Now}");
            writer.WriteLine($"Total shapes missing Alt text: {missingAltTextEntries.Count}");
            writer.WriteLine();

            foreach (string entry in missingAltTextEntries)
            {
                writer.WriteLine(entry);
            }
        }

        // Output summary to console
        Console.WriteLine("Alt text validation completed.");
        Console.WriteLine($"Total shapes missing Alt text: {missingAltTextEntries.Count}");
        Console.WriteLine($"Report saved to: {Path.GetFullPath(reportPath)}");
    }
}