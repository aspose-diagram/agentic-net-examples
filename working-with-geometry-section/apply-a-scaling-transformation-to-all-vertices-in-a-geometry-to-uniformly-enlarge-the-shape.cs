using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the uniform scaling factor (e.g., 2.0 for 200% enlargement)
                double scaleFactor = 2.0;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Each shape may contain one or more geometry sections (Geoms)
                        foreach (Geom geom in shape.Geoms)
                        {
                            // The CoordinateCol collection holds individual geometry vertices
                            foreach (var segment in geom.CoordinateCol)
                            {
                                // Scale MoveTo vertices
                                if (segment is MoveTo move)
                                {
                                    move.X.Value *= scaleFactor;
                                    move.Y.Value *= scaleFactor;
                                }
                                // Scale LineTo vertices
                                else if (segment is LineTo line)
                                {
                                    line.X.Value *= scaleFactor;
                                    line.Y.Value *= scaleFactor;
                                }
                                // Scale ArcTo vertices (if present)
                                else if (segment is ArcTo arc)
                                {
                                    arc.X.Value *= scaleFactor;
                                    arc.Y.Value *= scaleFactor;
                                }
                                // Scale other possible vertex types that expose X and Y (e.g., EllipticalArcTo)
                                else if (segment is EllipticalArcTo ellArc)
                                {
                                    ellArc.X.Value *= scaleFactor;
                                    ellArc.Y.Value *= scaleFactor;
                                }
                                // Add additional vertex types here if needed
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "scaled_output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Scaling completed. Saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }