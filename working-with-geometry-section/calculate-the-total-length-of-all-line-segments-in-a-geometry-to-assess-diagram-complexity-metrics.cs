using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                double totalLength = 0.0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through all geometry sections of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            double? prevX = null;
                            double? prevY = null;

                            // Iterate through all coordinate commands in the geometry
                            foreach (object segment in geom.CoordinateCol)
                            {
                                if (segment is MoveTo move)
                                {
                                    // Starting point of a new sub-path
                                    prevX = move.X.Value;
                                    prevY = move.Y.Value;
                                }
                                else if (segment is LineTo line)
                                {
                                    // Straight line segment: calculate distance from previous point
                                    if (prevX.HasValue && prevY.HasValue)
                                    {
                                        double dx = line.X.Value - prevX.Value;
                                        double dy = line.Y.Value - prevY.Value;
                                        totalLength += Math.Sqrt(dx * dx + dy * dy);
                                    }
                                    prevX = line.X.Value;
                                    prevY = line.Y.Value;
                                }
                                else if (segment is ArcTo arc)
                                {
                                    // Approximate arc length using chord length (simple fallback)
                                    if (prevX.HasValue && prevY.HasValue)
                                    {
                                        double dx = arc.X.Value - prevX.Value;
                                        double dy = arc.Y.Value - prevY.Value;
                                        double chord = Math.Sqrt(dx * dx + dy * dy);
                                        totalLength += chord;
                                    }
                                    prevX = arc.X.Value;
                                    prevY = arc.Y.Value;
                                }
                                // Other segment types (e.g., EllipticalArcTo, SplineStart, etc.) are ignored for this metric
                            }
                        }
                    }
                }

                Console.WriteLine($"Total length of all line segments (including simple arc approximations): {totalLength} inches");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }