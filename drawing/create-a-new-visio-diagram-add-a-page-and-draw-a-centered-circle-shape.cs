using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "CenteredCircle.vsdx";

        try
        {
            // Create a new empty diagram inside a using block
            using (Diagram diagram = new Diagram())
            {
                // Add a new page to the diagram
                Page newPage = new Page();
                diagram.Pages.Add(newPage);

                // Define circle parameters (center at (5,5) with radius 2)
                double centerX = 5.0;
                double centerY = 5.0;
                double radius = 2.0;
                double left = centerX - radius;
                double bottom = centerY - radius;
                double right = centerX + radius;
                double top = centerY + radius;

                // Draw the circle (ellipse with equal width and height) on the new page
                newPage.DrawEllipse(left, bottom, right, top);

                // Save the diagram to the specified file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}