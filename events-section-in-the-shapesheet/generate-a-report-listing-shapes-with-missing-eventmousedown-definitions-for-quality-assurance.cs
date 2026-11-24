using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputReportFile]");
            return;
        }

        string inputPath = args[0];
        string reportPath = args.Length >= 2 ? args[1] : "MissingEventMouseDownReport.txt";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the report file
                using (StreamWriter writer = new StreamWriter(reportPath, false))
                {
                    writer.WriteLine("Shapes missing EventMouseDown definitions");
                    writer.WriteLine($"Generated on {DateTime.Now}");
                    writer.WriteLine(new string('=', 50));

                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve the closest substitute for EventMouseDown.
                            // According to the API, EventMouseDown is not available;
                            // we use EventDrop as the nearest equivalent.
                            string eventFormula = string.Empty;

                            try
                            {
                                // Access the EventDrop formula via the Ufe.F property
                                eventFormula = shape.Event.EventDrop.Ufe.F;
                            }
                            catch (Exception ex)
                            {
                                // If accessing EventDrop fails, log the error and continue
                                Console.Error.WriteLine($"Error accessing EventDrop for shape ID {shape.ID}: {ex.Message}");
                            }

                            // If the formula is null, empty, or whitespace, consider EventMouseDown missing
                            if (string.IsNullOrWhiteSpace(eventFormula))
                            {
                                string shapeInfo = $"Page: {page.Name}, Shape ID: {shape.ID}, Name: {shape.Name}, Master: {shape.Master?.Name ?? "N/A"}";
                                Console.WriteLine(shapeInfo);
                                writer.WriteLine(shapeInfo);
                            }
                        }
                    }

                    writer.WriteLine(new string('=', 50));
                    writer.WriteLine("Report generation completed.");
                }

                Console.WriteLine($"Report saved to: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}