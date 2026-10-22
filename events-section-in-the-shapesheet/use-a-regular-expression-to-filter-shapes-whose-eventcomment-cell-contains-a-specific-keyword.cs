using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, keyword, optional output file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <keyword> [output.vsdx]");
            return;
        }

        string inputPath = args[0];
        string keyword = args[1];
        string outputPath = args.Length >= 3 ? args[2] : null;

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare the regular expression (case‑insensitive)
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

            Console.WriteLine($"Shapes with EventComment containing \"{keyword}\":");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Access the closest available event cell (EventDrop) as a substitute for EventComment
                    string eventFormula = shape.Event.EventDrop.Ufe.F;

                    // If the formula is null, skip this shape
                    if (string.IsNullOrEmpty(eventFormula))
                        continue;

                    // Apply the regular expression
                    if (regex.IsMatch(eventFormula))
                    {
                        // Output the shape ID and the matching formula
                        Console.WriteLine($"Page {page.ID}, Shape ID {shape.ID}: {eventFormula}");
                    }
                }
            }

            // If an output path is provided, save the (unchanged) diagram
            if (!string.IsNullOrEmpty(outputPath))
            {
                // Guard: ensure the directory for the output file exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
                    return;
                }

                // Save the diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}