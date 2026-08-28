using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, page name (or index), output CSV file
        if (args.Length < 3)
        {
            // Show usage information and exit gracefully instead of throwing
            Console.Error.WriteLine("Usage: ShapeInfoExtractor <inputVisioPath> <pageNameOrIndex> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string pageIdentifier = args[1];
        string outputCsvPath = args[2];

        try
        {
            // Load the diagram from the specified file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the target page (try by name first)
                Page page = diagram.Pages.GetPage(pageIdentifier);
                if (page == null)
                {
                    // If not found by name, attempt to parse as a zero‑based index
                    if (int.TryParse(pageIdentifier, out int pageIndex) &&
                        pageIndex >= 0 && pageIndex < diagram.Pages.Count)
                    {
                        page = diagram.Pages[pageIndex];
                    }
                    else
                    {
                        Console.Error.WriteLine($"Page '{pageIdentifier}' not found in the diagram.");
                        return;
                    }
                }

                // Open CSV writer for the output file (overwrite if exists)
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header line
                    writer.WriteLine("Name,PinX,PinY");

                    // Iterate over all shapes on the selected page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape name (fallback to empty string if null)
                        string shapeName = shape.Name ?? string.Empty;
                        // Retrieve shape position coordinates
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Write a CSV line with escaped name and coordinates
                        writer.WriteLine($"{EscapeCsv(shapeName)},{pinX},{pinY}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to escape commas, quotes, and line breaks in CSV fields
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            field = $"\"{field}\"";

        return field;
    }
}