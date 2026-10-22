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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            bool rectangleFound = false;

            // Iterate through pages and shapes to locate a rectangle.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check that the shape has a master named "Rectangle".
                    if (shape.Master != null && shape.Master.Name == "Rectangle")
                    {
                        // Set line dash pattern to dash‑dot.
                        shape.Line.LinePattern.Value = LinePatternValue.DashDot;

                        // Set line thickness to two points (2/72 inches).
                        shape.Line.LineWeight.Value = 2.0 / 72.0;

                        rectangleFound = true;
                        // Assuming only one rectangle needs modification.
                        break;
                    }
                }

                if (rectangleFound)
                    break;
            }

            if (!rectangleFound)
            {
                Console.Error.WriteLine("No rectangle shape was found in the diagram.");
                return;
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}