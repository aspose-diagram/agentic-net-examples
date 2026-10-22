using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputReportFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // StringBuilder to accumulate plain text from all pages.
            StringBuilder reportBuilder = new StringBuilder();

            // Iterate through each page in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Header for the current page.
                reportBuilder.AppendLine($"--- Page {pageIndex + 1} ---");

                // Iterate through each shape on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Retrieve the concatenated plain text of the shape.
                    string rawText = shape.Text.Value.Text;

                    // Skip shapes without text.
                    if (string.IsNullOrWhiteSpace(rawText))
                        continue;

                    // Remove any HTML tags that might be present.
                    string plainText = Regex.Replace(rawText, "<.*?>", string.Empty);

                    // Append the cleaned text to the report.
                    reportBuilder.AppendLine(plainText);
                }

                // Add a blank line after each page's content.
                reportBuilder.AppendLine();
            }

            // Write the concatenated text to the summary report file.
            File.WriteAllText(outputPath, reportBuilder.ToString());

            Console.WriteLine($"Summary report generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}