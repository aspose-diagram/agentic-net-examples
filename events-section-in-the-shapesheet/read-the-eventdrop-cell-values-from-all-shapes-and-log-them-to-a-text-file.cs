using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputLogFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the log file for writing.
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve the EventDrop formula string.
                            // According to the API, the formula is accessed via shape.Event.EventDrop.Ufe.F
                            string eventDropFormula = shape.Event?.EventDrop?.Ufe?.F;

                            // If the formula is null, treat it as empty.
                            if (eventDropFormula == null)
                                eventDropFormula = string.Empty;

                            // Log shape ID and its EventDrop value.
                            writer.WriteLine($"Shape ID: {shape.ID}, EventDrop: {eventDropFormula}");
                        }
                    }
                }
            }

            Console.WriteLine($"EventDrop values have been logged to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}