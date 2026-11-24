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
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputReportFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // StringBuilder to accumulate plain text from all pages.
        StringBuilder reportBuilder = new StringBuilder();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the concatenated plain text of the shape.
                    string rawText = shape.Text.Value.Text;

                    // Skip shapes without text.
                    if (string.IsNullOrWhiteSpace(rawText))
                        continue;

                    // Strip any HTML tags that might be present.
                    string plainText = Regex.Replace(rawText, "<.*?>", string.Empty);

                    // Append the cleaned text to the report, separating entries by a new line.
                    reportBuilder.AppendLine(plainText);
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while loading or processing the diagram.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        try
        {
            // Write the concatenated text to the summary report file.
            File.WriteAllText(outputPath, reportBuilder.ToString());
            Console.WriteLine($"Summary report generated successfully at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Report any errors that occurred while writing the report file.
            Console.Error.WriteLine($"Error writing report file: {ex.Message}");
        }
    }
}