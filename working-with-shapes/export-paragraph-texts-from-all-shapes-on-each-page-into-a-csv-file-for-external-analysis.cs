using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the output directory exists.
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Open a StreamWriter for the CSV output.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Write CSV header.
                    writer.WriteLine("PageNumber,ShapeID,ParagraphText");

                    // Iterate through pages using an index loop (avoid var in foreach over diagram.Pages).
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        // Retrieve the page.
                        Page page = diagram.Pages[pageIndex];
                        int pageNumber = pageIndex + 1; // Human‑readable page number.

                        // Iterate over all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Combine all text runs (Txt objects) belonging to the shape.
                            string combinedText = string.Empty;

                            if (shape.Text != null && shape.Text.Value != null)
                            {
                                foreach (FormatTxt fmt in shape.Text.Value)
                                {
                                    // Cast to Txt to access the actual text string.
                                    if (fmt is Txt txt)
                                    {
                                        combinedText += txt.Text;
                                    }
                                }
                            }

                            // Escape CSV fields to handle commas, quotes, and newlines.
                            string escapedText = EscapeCsv(combinedText);
                            string escapedShapeId = EscapeCsv(shape.ID.ToString());
                            string escapedPageNumber = EscapeCsv(pageNumber.ToString());

                            // Write a CSV line for the current shape.
                            writer.WriteLine($"{escapedPageNumber},{escapedShapeId},{escapedText}");
                        }
                    }
                }
            }

            Console.WriteLine($"Paragraph texts exported successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields according to RFC 4180.
    private static string EscapeCsv(string field)
    {
        if (field == null)
            return string.Empty;

        bool mustQuote = field.Contains("\"") || field.Contains(",") || field.Contains("\n") || field.Contains("\r");
        if (mustQuote)
        {
            string escaped = field.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        else
        {
            return field;
        }
    }
}