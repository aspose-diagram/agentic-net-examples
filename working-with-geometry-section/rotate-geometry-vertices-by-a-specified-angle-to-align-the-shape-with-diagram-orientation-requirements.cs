using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path to the output Visio file
                string outputPath = "output_rotated.vsdx";

                // Rotation angle in degrees (example: 45 degrees)
                double rotationAngleDeg = 45.0;

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Rotate geometry of every shape on every page
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        RotateShapeGeometry(shape, rotationAngleDeg);
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

        /// <summary>
        /// Rotates all geometry vertices of a shape around its PinX/PinY center.
        /// </summary>
        /// <param name="shape">The shape whose geometry will be rotated.</param>
        /// <param name="angleDeg">Rotation angle in degrees.</param>
        private static void RotateShapeGeometry(Shape shape, double angleDeg)
        {
            // Center of rotation (shape's pin point)
            double centerX = shape.XForm.PinX.Value;
            double centerY = shape.XForm.PinY.Value;

            // Convert angle to radians for Math functions
            double angleRad = angleDeg * Math.PI / 180.0;
            double cosTheta = Math.Cos(angleRad);
            double sinTheta = Math.Sin(angleRad);

            // Iterate through each geometry section of the shape
            foreach (Geom geom in shape.Geoms)
            {
                // Iterate through each coordinate segment (MoveTo, LineTo, etc.)
                for (int i = 0; i < geom.CoordinateCol.Count; i++)
                {
                    object segment = geom.CoordinateCol[i];

                    // Handle MoveTo segment
                    if (segment is MoveTo move)
                    {
                        RotatePoint(move.X, move.Y, centerX, centerY, cosTheta, sinTheta);
                    }
                    // Handle LineTo segment
                    else if (segment is LineTo line)
                    {
                        RotatePoint(line.X, line.Y, centerX, centerY, cosTheta, sinTheta);
                    }
                    // Handle ArcTo segment (if present)
                    else if (segment is ArcTo arc)
                    {
                        RotatePoint(arc.X, arc.Y, centerX, centerY, cosTheta, sinTheta);
                    }
                    // Additional segment types can be added here following the same pattern
                }
            }

            // Optionally update the shape's rotation angle cell to reflect the transformation
            shape.XForm.Angle.Value = angleDeg;
        }

        /// <summary>
        /// Rotates a point (represented by DoubleValue X and Y) around a center point.
        /// </summary>
        /// <param name="x">X coordinate (DoubleValue) to rotate.</param>
        /// <param name="y">Y coordinate (DoubleValue) to rotate.</param>
        /// <param name="centerX">X coordinate of the rotation center.</param>
        /// <param name="centerY">Y coordinate of the rotation center.</param>
        /// <param name="cosTheta">Cosine of the rotation angle.</param>
        /// <param name="sinTheta">Sine of the rotation angle.</param>
        private static void RotatePoint(DoubleValue x, DoubleValue y, double centerX, double centerY, double cosTheta, double sinTheta)
        {
            double originalX = x.Value;
            double originalY = y.Value;

            // Translate point to origin
            double translatedX = originalX - centerX;
            double translatedY = originalY - centerY;

            // Apply rotation matrix
            double rotatedX = translatedX * cosTheta - translatedY * sinTheta;
            double rotatedY = translatedX * sinTheta + translatedY * cosTheta;

            // Translate back to original center
            x.Value = rotatedX + centerX;
            y.Value = rotatedY + centerY;
        }
    }