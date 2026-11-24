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
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputLogPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                var sb = new StringBuilder();

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string rawText = shape.Text?.Value?.Text;

                        if (string.IsNullOrEmpty(rawText))
                            continue;

                        // Remove punctuation from the text
                        string sanitized = RemovePunctuation(rawText);

                        // Append sanitized text to the log content
                        sb.AppendLine(sanitized);
                    }
                }

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the collected text to the log file
                File.WriteAllText(outputPath, sb.ToString());
                Console.WriteLine($"Sanitized text has been saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    /// <summary>
    /// Removes all punctuation characters from the provided text.
    /// </summary>
    private static string RemovePunctuation(string text)
    {
        // Use a regular expression to strip punctuation and symbols
        return Regex.Replace(text, @"[\p{P}\p{S}]", string.Empty);
    }
}