using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Log page name (may be null).
                    Console.WriteLine($"Page Name: {page.Name ?? "Unnamed"}");

                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Log shape ID and name (may be null).
                        Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name ?? "Unnamed"}");

                        // Iterate through each geometry section of the shape.
                        foreach (Geom geom in shape.Geoms)
                        {
                            // Iterate through each segment in the geometry's coordinate collection.
                            foreach (object segment in geom.CoordinateCol)
                            {
                                // Handle MoveTo segment.
                                if (segment is MoveTo moveTo)
                                {
                                    double x = moveTo.X.Value;
                                    double y = moveTo.Y.Value;
                                    Console.WriteLine($"    MoveTo - X: {x}, Y: {y}");
                                }
                                // Handle LineTo segment.
                                else if (segment is LineTo lineTo)
                                {
                                    double x = lineTo.X.Value;
                                    double y = lineTo.Y.Value;
                                    Console.WriteLine($"    LineTo - X: {x}, Y: {y}");
                                }
                                // Handle ArcTo segment.
                                else if (segment is ArcTo arcTo)
                                {
                                    double x = arcTo.X.Value;
                                    double y = arcTo.Y.Value;
                                    Console.WriteLine($"    ArcTo - X: {x}, Y: {y}");
                                }
                                else
                                {
                                    // Unknown segment type – log its type name for debugging.
                                    Console.WriteLine($"    Unknown segment type: {segment.GetType().Name}");
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}