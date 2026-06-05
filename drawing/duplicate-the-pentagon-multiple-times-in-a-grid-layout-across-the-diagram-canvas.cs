using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page; add one if none exist
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define pentagon size
            double shapeWidth = 1.0;   // inches
            double shapeHeight = 1.0;  // inches

            // Define spacing between shapes
            double horizontalSpacing = shapeWidth + 0.5; // inches
            double verticalSpacing = shapeHeight + 0.5;  // inches

            // Calculate how many shapes fit horizontally and vertically
            int columns = (int)Math.Floor(pageWidth / horizontalSpacing);
            int rows = (int)Math.Floor(pageHeight / verticalSpacing);

            // Starting position (center of first shape)
            double startX = shapeWidth / 2.0 + 0.25;
            double startY = shapeHeight / 2.0 + 0.25;

            // Define pentagon vertices (relative to the shape's local coordinate system)
            // Points are supplied as a flat double array: x1, y1, x2, y2, ...
            double[] pentagonPoints = new double[]
            {
                0.0, 1.0,                     // Top vertex
                0.9511, 0.3090,               // Upper right
                0.5878, -0.8090,              // Lower right
                -0.5878, -0.8090,             // Lower left
                -0.9511, 0.3090               // Upper left
            };

            // Draw the pentagons in a grid
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    double pinX = startX + col * horizontalSpacing;
                    double pinY = startY + row * verticalSpacing;

                    // Draw a pentagon shape at the calculated position
                    // The method returns the shape ID (long), which we ignore here
                    page.DrawPolyline(pinX, pinY, shapeWidth, shapeHeight, pentagonPoints);
                }
            }

            // Save the diagram to a VSDX file
            diagram.Save("PentagonGrid.vsdx", SaveFileFormat.Vsdx);
        }
    }