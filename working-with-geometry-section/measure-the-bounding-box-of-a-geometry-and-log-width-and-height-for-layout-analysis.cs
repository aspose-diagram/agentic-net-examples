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
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages in the diagram
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];
                    // Fixed: Page.Name is a string, not a cell with .Value
                    Console.WriteLine($"Processing Page {pageIdx + 1}: \"{page.Name}\" with {page.Shapes.Count} shapes.");

                    // Iterate through all shapes on the current page
                    for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                    {
                        Shape shape = page.Shapes[shapeIdx];

                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has at least one geometry (Geoms collection)
                        if (shape.Geoms == null || shape.Geoms.Count == 0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has no geometry (Geoms) sections.");
                            continue;
                        }

                        double minX = double.MaxValue;
                        double maxX = double.MinValue;
                        double minY = double.MaxValue;
                        double maxY = double.MinValue;
                        bool hasCoordinates = false;

                        // Iterate over each geometry (Geom) in the shape
                        for (int geomIdx = 0; geomIdx < shape.Geoms.Count; geomIdx++)
                        {
                            // The Geoms collection returns objects; cast to Geom explicitly
                            Geom geom = (Geom)shape.Geoms[geomIdx];

                            // Iterate over each coordinate segment within the geometry
                            for (int coordIdx = 0; coordIdx < geom.CoordinateCol.Count; coordIdx++)
                            {
                                object segment = geom.CoordinateCol[coordIdx];
                                double x = 0.0;
                                double y = 0.0;

                                // Extract X and Y from known segment types
                                if (segment is MoveTo moveTo)
                                {
                                    x = moveTo.X.Value;
                                    y = moveTo.Y.Value;
                                }
                                else if (segment is LineTo lineTo)
                                {
                                    x = lineTo.X.Value;
                                    y = lineTo.Y.Value;
                                }
                                else if (segment is ArcTo arcTo)
                                {
                                    x = arcTo.X.Value;
                                    y = arcTo.Y.Value;
                                }
                                else if (segment is EllipticalArcTo ellArcTo)
                                {
                                    x = ellArcTo.X.Value;
                                    y = ellArcTo.Y.Value;
                                }
                                else if (segment is SplineKnot splineKnot)
                                {
                                    x = splineKnot.X.Value;
                                    y = splineKnot.Y.Value;
                                }
                                else
                                {
                                    // Unknown segment type; skip
                                    continue;
                                }

                                // Update bounding box extremes
                                if (x < minX) minX = x;
                                if (x > maxX) maxX = x;
                                if (y < minY) minY = y;
                                if (y > maxY) maxY = y;
                                hasCoordinates = true;
                            }
                        }

                        if (!hasCoordinates)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has geometry sections but no coordinate data.");
                            continue;
                        }

                        // Compute width and height of the bounding box (in inches)
                        double width = maxX - minX;
                        double height = maxY - minY;

                        Console.WriteLine($"Shape ID {shape.ID}: Geometry Bounding Box -> Width = {width:F3} in, Height = {height:F3} in");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
