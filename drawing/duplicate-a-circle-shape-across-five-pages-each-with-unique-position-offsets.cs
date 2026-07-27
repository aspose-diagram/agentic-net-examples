using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram (contains one default page)
            Diagram diagram = new Diagram();

            // Define circle parameters
            double circleWidth = 1.0;   // inches
            double circleHeight = 1.0;  // inches (equal width => circle)
            double startPinX = 2.0;     // starting X position
            double startPinY = 2.0;     // starting Y position
            double offsetX = 2.5;       // horizontal offset between pages
            double offsetY = 2.5;       // vertical offset between pages

            // Ensure we have exactly five pages and draw a circle on each with unique offsets
            for (int i = 0; i < 5; i++)
            {
                // Use existing first page for i == 0, otherwise add a new page
                Page page;
                if (i == 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Calculate position for this page
                double pinX = startPinX + i * offsetX;
                double pinY = startPinY + i * offsetY;

                // Draw the circle (ellipse with equal width and height)
                long shapeId = page.DrawEllipse(pinX, pinY, circleWidth, circleHeight);

                // Optional: set a fill color for visual distinction
                Shape circle = page.Shapes.GetShape(shapeId);
                circle.Fill.FillForegnd.Value = "#00AAFF"; // light blue fill
            }

            // Save the diagram to a VSDX file
            diagram.Save("CircleDiagram.vsdx", SaveFileFormat.Vsdx);
        }
    }