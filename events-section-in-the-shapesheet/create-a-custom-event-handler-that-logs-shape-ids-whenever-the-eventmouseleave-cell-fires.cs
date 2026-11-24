using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape in the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the EventDrop formula as a substitute for EventMouseLeave.
                    // The formula calls a (hypothetical) macro "LogShapeId" which should log the shape's ID.
                    // In a real Visio environment, you would define this macro in the VBA project.
                    shape.Event.EventDrop.Ufe.F = $"CALLTHIS(\"LogShapeId({shape.ID})\")";
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}