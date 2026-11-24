using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                double totalLength = 0.0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through each geometry (Geom) of the shape
                        for (int g = 0; g < shape.Geoms.Count; g++)
                        {
                            // Cast the geometry object to Geom
                            Geom geom = (Geom)shape.Geoms[g];

                            // Access the collection of coordinates within the geometry
                            var coordinates = geom.CoordinateCol;

                            // Sum distances between consecutive coordinate points
                            for (int i = 1; i < coordinates.Count; i++)
                            {
                                // Use dynamic to access X and Y properties of the coordinate objects
                                dynamic prev = coordinates[i - 1];
                                dynamic curr = coordinates[i];

                                double x1 = (double)prev.X.Value;
                                double y1 = (double)prev.Y.Value;
                                double x2 = (double)curr.X.Value;
                                double y2 = (double)curr.Y.Value;

                                double dx = x2 - x1;
                                double dy = y2 - y1;
                                totalLength += Math.Sqrt(dx * dx + dy * dy);
                            }
                        }
                    }
                }

                // Output the calculated total length
                Console.WriteLine($"Total length of all line segments: {totalLength}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}