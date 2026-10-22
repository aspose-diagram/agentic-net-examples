using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output log file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "EventDropLog.txt";

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

            // Open a StreamWriter for the log file
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the EventDrop formula if available
                        string eventDropFormula = null;
                        if (shape.Event != null && shape.Event.EventDrop != null && shape.Event.EventDrop.Ufe != null)
                        {
                            eventDropFormula = shape.Event.EventDrop.Ufe.F;
                        }

                        // Write the information to the log file
                        writer.WriteLine($"Page {page.ID}, Shape {shape.ID}: EventDrop = {eventDropFormula ?? "null"}");
                    }
                }
            }

            Console.WriteLine($"EventDrop values have been logged to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}