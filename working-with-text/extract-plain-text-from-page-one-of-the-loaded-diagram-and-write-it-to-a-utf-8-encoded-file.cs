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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputTextPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page (page index 0).
                Page page = diagram.Pages[0];

                // StringBuilder to accumulate extracted plain text.
                StringBuilder sb = new StringBuilder();

                // Iterate over all shapes on the first page.
                foreach (Shape shape in page.Shapes)
                {
                    // Guard against null Text objects.
                    if (shape.Text != null && shape.Text.Value != null)
                    {
                        // Extract the raw text from the shape.
                        string rawText = shape.Text.Value.Text;

                        // Skip empty strings.
                        if (!string.IsNullOrEmpty(rawText))
                        {
                            // Remove any HTML or XML tags to obtain plain text.
                            string plainText = Regex.Replace(rawText, "<.*?>", string.Empty);

                            // Append the cleaned text followed by a newline.
                            sb.AppendLine(plainText);
                        }
                    }
                }

                // Write the accumulated text to the output file using UTF‑8 encoding.
                File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}