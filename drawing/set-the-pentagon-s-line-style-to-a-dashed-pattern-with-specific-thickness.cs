using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Define points for a regular pentagon (flat double array: x1, y1, x2, y2, ...)
            double[] pentagonPoints = new double[]
            {
                5.0, 3.0,   // Point 1
                6.9, 4.0,   // Point 2
                6.1, 6.9,   // Point 3
                3.9, 6.9,   // Point 4
                3.1, 4.0,   // Point 5
                5.0, 3.0    // Close the polygon by returning to Point 1
            };

            // Draw the pentagon on the page
            page.DrawPolyline(pentagonPoints);

            // Retrieve the shape that was just added (the first shape on the page)
            Shape pentagonShape = null;
            foreach (Shape shape in page.Shapes)
            {
                pentagonShape = shape;
                break; // we only need the first shape
            }

            if (pentagonShape != null)
            {
                // Set line pattern to dashed
                pentagonShape.Line.LinePattern.Value = LinePatternValue.Dash;

                // Set line thickness (weight) – value is in inches
                pentagonShape.Line.LineWeight.Value = 0.03; // example thickness
            }
            else
            {
                throw new Exception("Pentagon shape was not created.");
            }

            // Save the diagram to a VSDX file
            diagram.Save("Pentagon.vsdx", SaveFileFormat.Vsdx);
        }
    }