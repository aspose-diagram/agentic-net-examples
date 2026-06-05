using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the active page where shapes will be drawn
            Page page = diagram.ActivePage;

            // Define the common center for all circles
            double centerX = 5.0;
            double centerY = 5.0;

            // Starting radius and increment for each successive circle
            double startRadius = 0.5;
            double radiusIncrement = 0.5;

            // Draw 50 circles with incremental radii
            for (int i = 0; i < 50; i++)
            {
                double radius = startRadius + i * radiusIncrement;
                double width = radius * 2;   // Circle width = diameter
                double height = radius * 2;  // Circle height = diameter

                // Draw an ellipse (circle) on the page
                page.DrawEllipse(centerX, centerY, width, height);
            }

            // Export the diagram to a PDF file
            diagram.Save("Circles.pdf", SaveFileFormat.Pdf);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
