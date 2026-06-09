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
                        // Iterate through each geometry section of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            double prevX = 0.0;
                            double prevY = 0.0;
                            bool hasPrev = false;

                            // Iterate through the coordinate collection (MoveTo, LineTo, etc.)
                            foreach (var segment in geom.CoordinateCol)
                            {
                                if (segment is MoveTo move)
                                {
                                    // Set the starting point for subsequent LineTo segments
                                    prevX = move.X.Value;
                                    prevY = move.Y.Value;
                                    hasPrev = true;
                                }
                                else if (segment is LineTo line && hasPrev)
                                {
                                    // Calculate Euclidean distance between current point and previous point
                                    double dx = line.X.Value - prevX;
                                    double dy = line.Y.Value - prevY;
                                    double segmentLength = Math.Sqrt(dx * dx + dy * dy);
                                    totalLength += segmentLength;

                                    // Update previous point
                                    prevX = line.X.Value;
                                    prevY = line.Y.Value;
                                }
                                // Other segment types (ArcTo, EllipticalArcTo, etc.) are ignored for this metric
                            }
                        }
                    }
                }

                // Output the total length of all line segments
                Console.WriteLine($"Total length of all line segments: {totalLength}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }