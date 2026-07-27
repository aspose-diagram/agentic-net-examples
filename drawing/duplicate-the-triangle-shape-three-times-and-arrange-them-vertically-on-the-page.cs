using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty Visio diagram.
            using (Diagram diagram = new Diagram())
            {
                // Get the first (default) page.
                Page page = diagram.Pages[0];

                // Base triangle vertices (pin points) – an upright triangle.
                // Points are defined as a flat double array: x1, y1, x2, y2, x3, y3, x1, y1 (closed shape).
                double[] baseTriangle = new double[] { 2.0, 2.0, 4.0, 2.0, 3.0, 4.0, 2.0, 2.0 };

                // Draw the original triangle.
                page.DrawPolyline(baseTriangle);

                // Number of duplicates to create.
                int duplicateCount = 3;
                // Vertical spacing between each triangle (in inches).
                double verticalSpacing = 5.0;

                // Create duplicates by shifting the Y‑coordinates of the base points.
                for (int i = 1; i <= duplicateCount; i++)
                {
                    double offsetY = i * verticalSpacing;
                    double[] shiftedTriangle = new double[baseTriangle.Length];

                    // Copy X values unchanged, add offset to Y values.
                    for (int j = 0; j < baseTriangle.Length; j += 2)
                    {
                        shiftedTriangle[j] = baseTriangle[j];               // X coordinate
                        shiftedTriangle[j + 1] = baseTriangle[j + 1] + offsetY; // Y coordinate with offset
                    }

                    // Draw the duplicated triangle.
                    page.DrawPolyline(shiftedTriangle);
                }

                // Save the diagram to a VSDX file.
                diagram.Save("TriangleDuplicates.vsdx", SaveFileFormat.Vsdx);
            }
        }
    }