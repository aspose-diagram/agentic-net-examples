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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a StringBuilder for CSV content.
                StringBuilder csvBuilder = new StringBuilder();

                // Write CSV header.
                csvBuilder.AppendLine("PageNumber,TextContent");

                int pageNumber = 1; // 1‑based page counter.

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Accumulate plain text from all shapes on the current page.
                    StringBuilder pageTextBuilder = new StringBuilder();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape.
                        // According to the API, shape.Text.Value.Text provides the text.
                        string shapeText = shape.Text?.Value?.Text ?? string.Empty;

                        if (!string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Append a space to separate texts from different shapes.
                            if (pageTextBuilder.Length > 0)
                                pageTextBuilder.Append(' ');
                            pageTextBuilder.Append(shapeText);
                        }
                    }

                    // Combine the collected text and strip any HTML tags (if present).
                    string rawPageText = pageTextBuilder.ToString();
                    string plainPageText = Regex.Replace(rawPageText, "<.*?>", string.Empty);

                    // Escape double quotes for CSV compliance.
                    string escapedText = plainPageText.Replace("\"", "\"\"");

                    // Write the CSV line: page number (1‑based) and escaped text enclosed in quotes.
                    csvBuilder.AppendLine($"{pageNumber},\"{escapedText}\"");

                    pageNumber++;
                }

                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputCsvPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Write the CSV content to the specified file.
                File.WriteAllText(outputCsvPath, csvBuilder.ToString(), Encoding.UTF8);
                Console.WriteLine($"CSV file successfully created at \"{outputCsvPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}