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
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputTextFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "extracted_text.txt";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                var sb = new StringBuilder();

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape.
                        string text = shape.Text?.Value?.Text ?? string.Empty;

                        // Skip empty or whitespace‑only text.
                        if (string.IsNullOrWhiteSpace(text))
                            continue;

                        // Filter out numeric strings (e.g., "12345").
                        if (Regex.IsMatch(text.Trim(), @"^\d+$"))
                            continue;

                        // Append the cleaned text to the result.
                        sb.AppendLine(text.Trim());
                    }
                }

                // Write the aggregated text to the output file.
                File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
                Console.WriteLine($"Extracted text saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}