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

                // Input Visio file path (first argument) or default.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output CSV file path (second argument) or default.
                string outputCsv = args.Length > 1 ? args[1] : "output.csv";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // StringBuilder to accumulate CSV lines.
                StringBuilder csvBuilder = new StringBuilder();

                // Iterate through pages using explicit type (no var).
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Collect all plain text from shapes on this page.
                    StringBuilder pageTextBuilder = new StringBuilder();

                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text of the shape.
                        string shapeText = shape.Text.Value.Text;

                        // Skip empty or whitespace-only text.
                        if (string.IsNullOrWhiteSpace(shapeText))
                            continue;

                        // Replace line breaks and commas to keep CSV format clean.
                        shapeText = shapeText.Replace("\r\n", " ")
                                             .Replace("\n", " ")
                                             .Replace(",", " ");

                        pageTextBuilder.Append(shapeText).Append(' ');
                    }

                    // Trim trailing space.
                    string pageText = pageTextBuilder.ToString().Trim();

                    // Escape double quotes for CSV compliance.
                    pageText = pageText.Replace("\"", "\"\"");

                    // CSV format: PageNumber, "TextContent"
                    csvBuilder.AppendLine($"{pageIndex + 1},\"{pageText}\"");
                }

                // Write the CSV content to the output file.
                File.WriteAllText(outputCsv, csvBuilder.ToString());

                Console.WriteLine($"Extraction completed. CSV saved to: {outputCsv}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }