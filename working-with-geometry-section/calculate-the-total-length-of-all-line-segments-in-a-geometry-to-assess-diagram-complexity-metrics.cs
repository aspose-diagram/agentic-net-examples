using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

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

                            // CoordinateCol contains drawing commands such as MoveTo, LineTo, etc.
                            foreach (object cmd in geom.CoordinateCol)
                            {
                                if (cmd is MoveTo move)
                                {
                                    // MoveTo sets the starting point for subsequent line segments
                                    prevX = move.X.Value;
                                    prevY = move.Y.Value;
                                    hasPrev = true;
                                }
                                else if (cmd is LineTo line)
                                {
                                    // LineTo draws a line from the previous point to the current point
                                    if (hasPrev)
                                    {
                                        double dx = line.X.Value - prevX;
                                        double dy = line.Y.Value - prevY;
                                        totalLength += Math.Sqrt(dx * dx + dy * dy);
                                    }

                                    // Update previous point for the next segment
                                    prevX = line.X.Value;
                                    prevY = line.Y.Value;
                                    hasPrev = true;
                                }
                                // Other command types (ArcTo, EllipticalArcTo, etc.) are ignored for simplicity
                            }
                        }
                    }
                }

                // Output the total length of all line segments (in inches)
                Console.WriteLine($"Total length of line segments: {totalLength} inches");

                // Optionally save the diagram unchanged (demonstrates save usage)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }