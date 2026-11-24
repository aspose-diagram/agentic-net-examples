using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define output PDF file path
        string outputPath = "Circles.pdf";

        try
        {
            // Create a new blank diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Ensure there is at least one page to draw on
                Page page = diagram.Pages[0];

                // Parameters for circle placement
                double startX = 1.0;
                double startY = 1.0;
                double radiusStep = 0.5;

                // Generate 50 circles with incremental radii
                for (int i = 1; i <= 50; i++)
                {
                    double radius = i * radiusStep;

                    // Calculate top‑left corner of the bounding box for the circle
                    double left = startX + i * 2.0;   // offset each circle to avoid overlap
                    double top = startY + i * 2.0;

                    // Draw an ellipse where width and height are equal (a circle)
                    // DrawEllipse(pinX, pinY, width, height)
                    page.DrawEllipse(left, top, radius * 2, radius * 2);
                }

                // Save the diagram as PDF using the required overload
                diagram.Save(outputPath, SaveFileFormat.Pdf);
            }

            Console.WriteLine($"Diagram successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}