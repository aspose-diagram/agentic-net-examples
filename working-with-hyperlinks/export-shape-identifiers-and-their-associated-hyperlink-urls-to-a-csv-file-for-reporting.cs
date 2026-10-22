using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input Visio file path and output CSV file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("ShapeId,HyperlinkAddress");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Iterate through each hyperlink associated with the shape.
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Retrieve the address (URL) of the hyperlink.
                                string address = link.Address?.Value ?? string.Empty;

                                // Write a CSV line with shape ID and hyperlink address.
                                writer.WriteLine($"{shape.ID},{EscapeCsv(address)}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed successfully. CSV saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields that may contain commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
        {
            field = field.Replace("\"", "\"\"");
        }
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
        {
            field = $"\"{field}\"";
        }
        return field;
    }
}