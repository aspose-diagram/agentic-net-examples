using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Access the closest substitute for EventMouseDown.
                        // According to the API, EventMouseDown does not exist, so we use EventDrop as the nearest equivalent.
                        string eventFormula = shape.Event?.EventDrop?.Ufe?.F;

                        // If the formula is not empty, log the shape ID and the formula.
                        if (!string.IsNullOrEmpty(eventFormula))
                        {
                            // Shape.ID provides the identifier of the shape.
                            Console.WriteLine($"Shape ID: {shape.ID}, EventMouseDown (substituted) formula: {eventFormula}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}