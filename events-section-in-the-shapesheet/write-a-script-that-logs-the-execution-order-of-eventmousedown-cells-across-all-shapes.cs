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

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the formula for the EventMouseDown cell.
                    // Aspose.Diagram does not expose EventMouseDown directly,
                    // so we use the closest available cell: EventDrop.
                    string eventFormula = shape.Event.EventDrop.Ufe.F;

                    // Log the execution order (page index, shape ID, formula)
                    Console.WriteLine($"Page {pageIndex}, Shape ID {shape.ID}, EventMouseDown formula: {eventFormula}");
                }

                pageIndex++;
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}