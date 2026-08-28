using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the scaled output file
                string outputPath = "output_scaled.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Uniform scaling factor (e.g., 2.0 doubles the size)
                double scaleFactor = 2.0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through each geometry section of the shape
                        foreach (Geom geom in shape.Geoms)
                        {
                            // Iterate through each vertex/segment in the geometry
                            foreach (object segmentObj in geom.CoordinateCol)
                            {
                                // Handle MoveTo vertices
                                if (segmentObj is MoveTo moveTo)
                                {
                                    moveTo.X.Value *= scaleFactor;
                                    moveTo.Y.Value *= scaleFactor;
                                }
                                // Handle LineTo vertices
                                else if (segmentObj is LineTo lineTo)
                                {
                                    lineTo.X.Value *= scaleFactor;
                                    lineTo.Y.Value *= scaleFactor;
                                }
                                // Additional segment types (ArcTo, EllipticalArcTo, etc.) can be handled similarly if needed
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }