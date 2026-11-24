using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args == null || args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header.
                writer.WriteLine("ShapeID,Width,Height,ConnectionPoints");

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape identifier.
                        long shapeId = shape.ID;

                        // Retrieve final width and height (values are stored in the XForm cell collection).
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Count how many connections involve this shape.
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

            Console.WriteLine($"Summary CSV has been created at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations or file I/O.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}