using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: ShapeEventReport <visio-file-path>");
            return;
        }

        string visioPath = args[0];
        // Verify the input file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            Console.WriteLine("Shapes missing EventDrop definitions:");

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Access the EventDrop formula (as a representative event cell).
                    // The formula string is stored in the Ufe.F property.
                    string eventDropFormula = shape.Event.EventDrop?.Ufe?.F;

                    // If the formula is null, empty, or whitespace, report the shape.
                    if (string.IsNullOrWhiteSpace(eventDropFormula))
                    {
                        Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, Name: {shape.NameU}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}