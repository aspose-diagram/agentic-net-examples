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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeID,Width,Height,ConnectionCount");

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifier.
                        long shapeId = shape.ID;

                        // Retrieve final width and height from the shape's XForm cell values.
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Count how many connections involve this shape (as source or target).
                        int connectionCount = 0;
                        foreach (Connect conn in page.Connects)
                        {
                            if (conn.FromSheet == shapeId || conn.ToSheet == shapeId)
                            {
                                connectionCount++;
                            }
                        }

                        // Write the shape information as a CSV line.
                        writer.WriteLine($"{shapeId},{width},{height},{connectionCount}");
                    }
                }
            }

            Console.WriteLine($"Summary CSV has been saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}