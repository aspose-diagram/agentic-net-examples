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

        // Guard: ensure the Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write the CSV file.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeId,Angle");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Retrieve the shape's unique ID (long) and rotation angle (degrees).
                        long shapeId = shape.ID;
                        double angle = shape.XForm.Angle.Value; // Angle is stored in degrees.

                        // Write a CSV line for this shape.
                        writer.WriteLine($"{shapeId},{angle}");
                    }
                }
            }

            Console.WriteLine($"CSV file successfully created at \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}