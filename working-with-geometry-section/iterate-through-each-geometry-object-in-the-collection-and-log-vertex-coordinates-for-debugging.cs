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
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        int geomIndex = 0;

                        // Iterate through each Geometry object of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            int segmentIndex = 0;

                            // Iterate through each vertex/segment in the geometry's coordinate collection
                            foreach (object segment in geom.CoordinateCol)
                            {
                                string segmentType = segment.GetType().Name;
                                double x = double.NaN;
                                double y = double.NaN;

                                // Extract X and Y values based on the concrete segment type
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
                                // Additional segment types can be handled here as needed

                                // Log the vertex coordinates for debugging
                                Console.WriteLine(
                                    $"Page {page.ID}, Shape {shape.ID}, Geom {geomIndex}, Segment {segmentIndex} ({segmentType}) - X: {x}, Y: {y}");

                                segmentIndex++;
                            }

                            geomIndex++;
                        }
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