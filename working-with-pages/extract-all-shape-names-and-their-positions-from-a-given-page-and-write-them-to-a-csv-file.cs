using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <pageIndex> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string pageIndexArg = args[1];
        string outputCsvPath = args[2];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Guard: parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Page index must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Guard: ensure the requested page exists.
            if (pageIndex >= diagram.Pages.Count)
            {
                Console.Error.WriteLine($"Error: Diagram contains {diagram.Pages.Count} page(s); requested index {pageIndex} is out of range.");
                return;
            }

            // Retrieve the specified page.
            Page page = diagram.Pages[pageIndex];

            // Prepare to write CSV output.
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Write CSV header.
                writer.WriteLine("ShapeName,PinX,PinY");

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape name (universal name) and position.
                    string shapeName = shape.NameU ?? string.Empty;
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    // Write a CSV line. Use invariant culture to ensure '.' as decimal separator.
                    writer.WriteLine($"{EscapeCsv(shapeName)},{pinX.ToString(System.Globalization.CultureInfo.InvariantCulture)},{pinY.ToString(System.Globalization.CultureInfo.InvariantCulture)}");
                }
            }

            Console.WriteLine($"Shape information successfully written to \"{outputCsvPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // Helper method to escape CSV fields that may contain commas or quotes.
    private static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n") || field.Contains("\r"))
            return $"\"{field}\"";

        return field;
    }
}