using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the active page (the first page)
                Page page = diagram.ActivePage;

                // Define points for a diamond shape (top, right, bottom, left, back to top)
                // Coordinates are in inches
                double[] diamondPoints = new double[]
                {
                    5.0, 7.0,   // Top
                    7.0, 5.0,   // Right
                    5.0, 3.0,   // Bottom
                    3.0, 5.0,   // Left
                    5.0, 7.0    // Close the polygon
                };

                // Draw the diamond polyline; this returns the shape ID
                long shapeId = page.DrawPolyline(diamondPoints);

                // Retrieve the shape object using the returned ID
                Shape diamondShape = page.Shapes.GetShape((int)shapeId);

                // Set a thick border (line weight) and color
                diamondShape.Line.LineWeight.Value = 0.05; // Thickness in inches
                diamondShape.Line.LineColor.Value = "#FF0000"; // Red border

                // Export the entire diagram (containing the diamond) as SVG
                SVGSaveOptions svgOptions = new SVGSaveOptions();
                diagram.Save("diamond.svg", svgOptions);

                Console.WriteLine("Diamond shape exported to diamond.svg");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }