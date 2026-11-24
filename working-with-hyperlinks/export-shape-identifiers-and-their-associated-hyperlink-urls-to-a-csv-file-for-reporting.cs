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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Prepare the CSV writer.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("ShapeId,HyperlinkAddress");

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Retrieve the shape identifier.
                        long shapeId = shape.ID;

                        // Ensure the Hyperlinks collection is not null.
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            // Enumerate each hyperlink associated with the shape.
                            for (int linkIndex = 0; linkIndex < shape.Hyperlinks.Count; linkIndex++)
                            {
                                Hyperlink link = shape.Hyperlinks[linkIndex];

                                // Extract the address (URL) of the hyperlink.
                                string address = link.Address != null ? link.Address.Value : string.Empty;

                                // Write a CSV line for this shape‑hyperlink pair.
                                writer.WriteLine($"{shapeId},{address}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed successfully. CSV saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}