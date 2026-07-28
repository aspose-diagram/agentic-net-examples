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
                // Path to the rotated output file
                string outputPath = "rotated_output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Rotation angle in degrees
                double angleDeg = 45.0;
                // Convert to radians because the geometry uses Cartesian coordinates
                double angleRad = Math.PI * angleDeg / 180.0;
                double cos = Math.Cos(angleRad);
                double sin = Math.Sin(angleRad);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Center of rotation (shape's PinX/PinY)
                        double centerX = shape.XForm.PinX.Value;
                        double centerY = shape.XForm.PinY.Value;

                        // Iterate through each geometry section of the shape
                        for (int g = 0; g < shape.Geoms.Count; g++)
                        {
                            Geom geom = (Geom)shape.Geoms[g];

                            // Iterate through each coordinate (MoveTo, LineTo, ArcTo, etc.)
                            for (int c = 0; c < geom.CoordinateCol.Count; c++)
                            {
                                object coord = geom.CoordinateCol[c];

                                if (coord is MoveTo)
                                {
                                    MoveTo mt = (MoveTo)coord;
                                    RotatePoint(mt, centerX, centerY, cos, sin);
                                }
                                else if (coord is LineTo)
                                {
                                    LineTo lt = (LineTo)coord;
                                    RotatePoint(lt, centerX, centerY, cos, sin);
                                }
                                else if (coord is ArcTo)
                                {
                                    ArcTo at = (ArcTo)coord;
                                    RotatePoint(at, centerX, centerY, cos, sin);
                                }
                                // Add other coordinate types if needed (e.g., EllipticalArcTo, SplineKnot, etc.)
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

        // Helper method to rotate a point defined by X and Y DoubleValue objects
        private static void RotatePoint(object pointObj, double cx, double cy, double cos, double sin)
        {
            // All coordinate types have X and Y properties of type DoubleValue
            dynamic pt = pointObj;
            double originalX = pt.X.Value;
            double originalY = pt.Y.Value;

            double dx = originalX - cx;
            double dy = originalY - cy;

            double rotatedX = cx + (dx * cos - dy * sin);
            double rotatedY = cy + (dx * sin + dy * cos);

            pt.X.Value = rotatedX;
            pt.Y.Value = rotatedY;
        }
    }