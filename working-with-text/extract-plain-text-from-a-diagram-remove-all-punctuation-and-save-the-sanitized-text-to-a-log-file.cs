using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputLogPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "sanitized_text.log";

        // Guard: ensure the input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // StringBuilder to accumulate sanitized text
        StringBuilder sanitizedBuilder = new StringBuilder();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Guard: skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string rawText = shape.Text?.Value?.Text ?? string.Empty;

                        // Skip empty text
                        if (string.IsNullOrWhiteSpace(rawText))
                            continue;

                        // Remove all punctuation using a regular expression
                        // This keeps letters, digits and whitespace
                        string sanitized = Regex.Replace(rawText, @"[^\w\s]", string.Empty);

                        // Append the sanitized line to the builder
                        sanitizedBuilder.AppendLine(sanitized);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        try
        {
            // Write the accumulated sanitized text to the log file
            File.WriteAllText(outputPath, sanitizedBuilder.ToString());
            Console.WriteLine($"Sanitized text saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing log file: {ex.Message}");
        }
    }
}