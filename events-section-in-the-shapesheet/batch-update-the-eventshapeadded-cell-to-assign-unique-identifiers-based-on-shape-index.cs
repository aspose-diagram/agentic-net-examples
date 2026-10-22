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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Counter for generating unique identifiers
            int shapeCounter = 1;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                // Retrieve each shape by its ID using the Shapes collection
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an Event section
                    if (shape.Event != null)
                    {
                        // Assign a unique identifier to the closest equivalent event cell (EventDrop)
                        // The formula must be a quoted string, e.g., "Shape_1"
                        shape.Event.EventDrop.Ufe.F = $"\"Shape_{shapeCounter}\"";
                    }

                    shapeCounter++;
                }
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}