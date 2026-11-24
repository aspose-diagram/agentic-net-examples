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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare the CSV output file.
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    // Write CSV header.
                    writer.WriteLine("ShapeID,PinX,PinY,Width,Height");

                    // Iterate through all pages in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve shape identifier.
                            long shapeId = shape.ID;

                            // Retrieve bounding rectangle data from the shape's XForm.
                            double pinX = shape.XForm.PinX.Value;
                            double pinY = shape.XForm.PinY.Value;
                            double width = shape.XForm.Width.Value;
                            double height = shape.XForm.Height.Value;

                            // Write a CSV line for the current shape.
                            writer.WriteLine($"{shapeId},{pinX},{pinY},{width},{height}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}