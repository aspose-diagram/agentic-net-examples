using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input diagram path and output text file path as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var sb = new StringBuilder();

                // Iterate through all pages and shapes to collect plain text
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape; skip if null or empty
                        string rawText = shape.Text?.Value?.Text;
                        if (string.IsNullOrWhiteSpace(rawText))
                            continue;

                        // Remove any HTML tags that might be present
                        string cleanedText = Regex.Replace(rawText, "<.*?>", string.Empty).Trim();

                        // Filter out strings that consist only of digits
                        if (Regex.IsMatch(cleanedText, @"^\d+$"))
                            continue;

                        // Append the cleaned, non‑numeric text to the result
                        sb.AppendLine(cleanedText);
                    }
                }

                // Write the aggregated text to the specified output file
                File.WriteAllText(outputPath, sb.ToString());
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}