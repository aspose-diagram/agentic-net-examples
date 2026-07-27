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

            // Create a new Visio diagram
            using (Diagram diagram = new Diagram())
            {
                // Get the active page where shapes will be drawn
                Page page = diagram.ActivePage;

                // Center point for all circles
                double centerX = 200.0;
                double centerY = 200.0;

                // Incremental radius step
                double radiusStep = 5.0;

                // Draw 50 circles with increasing radii
                for (int i = 1; i <= 50; i++)
                {
                    double radius = i * radiusStep;
                    double width = radius * 2;
                    double height = radius * 2;

                    // DrawEllipse draws an ellipse; using equal width/height creates a circle
                    page.DrawEllipse(centerX, centerY, width, height);
                }

                // Save the diagram as PDF
                diagram.Save("Circles.pdf", SaveFileFormat.Pdf);
            }

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
