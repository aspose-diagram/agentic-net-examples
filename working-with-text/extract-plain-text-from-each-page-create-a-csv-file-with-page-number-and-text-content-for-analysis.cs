using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsv = "output.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Prepare CSV content
                var csvBuilder = new StringBuilder();
                csvBuilder.AppendLine("PageNumber,TextContent");

                int pageNumber = 1;
                foreach (Page page in diagram.Pages)
                {
                    var pageTextBuilder = new StringBuilder();

                    // Iterate all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get plain text from the shape
                        string text = shape.Text.Value.Text;

                        // Append non‑empty text
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            // Replace line breaks with spaces for CSV readability
                            text = text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
                            pageTextBuilder.Append(text);
                            pageTextBuilder.Append(' ');
                        }
                    }

                    // Combine and clean up the page text
                    string pageText = pageTextBuilder.ToString().Trim();

                    // Escape double quotes for CSV format
                    pageText = pageText.Replace("\"", "\"\"");

                    // Write the CSV line (text is quoted to preserve commas)
                    csvBuilder.AppendLine($"{pageNumber},\"{pageText}\"");

                    pageNumber++;
                }

                // Write CSV to file
                File.WriteAllText(outputCsv, csvBuilder.ToString());

                Console.WriteLine($"Text extraction complete. CSV saved to: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }