using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file. Provide via command line or replace with a literal path.
            string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

            // Load the diagram.
            Diagram diagram = new Diagram(diagramPath);

            double totalLength = 0.0;

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Each shape can have multiple geometry sections.
                    foreach (Geom geom in shape.Geoms)
                    {
                        double prevX = 0.0;
                        double prevY = 0.0;
                        bool hasPrev = false;

                        // Iterate through the coordinate collection.
                        foreach (object item in geom.CoordinateCol)
                        {
                            if (item is MoveTo move)
                            {
                                // MoveTo defines a new starting point; no length added.
                                prevX = move.X.Value;
                                prevY = move.Y.Value;
                                hasPrev = true;
                            }
                            else if (item is LineTo line)
                            {
                                // LineTo defines a straight segment from the previous point.
                                double x = line.X.Value;
                                double y = line.Y.Value;

                                if (hasPrev)
                                {
                                    double dx = x - prevX;
                                    double dy = y - prevY;
                                    totalLength += Math.Sqrt(dx * dx + dy * dy);
                                }

                                // Update previous point for subsequent segments.
                                prevX = x;
                                prevY = y;
                                hasPrev = true;
                            }
                            // Other geometry commands (ArcTo, etc.) are ignored for length calculation.
                        }
                    }
                }
            }

            Console.WriteLine($"Total length of all line segments: {totalLength}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
