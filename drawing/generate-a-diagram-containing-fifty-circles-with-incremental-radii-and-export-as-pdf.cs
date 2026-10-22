using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output PDF file path
        string outputPath = "Circles.pdf";

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to draw on
            if (diagram.Pages.Count == 0)
            {
                // Create a new page with ID = 1 and add it to the diagram
                Page newPage = new Page(1);
                diagram.Pages.Add(newPage);
            }

            // Use the first page for drawing
            Page page = diagram.Pages[0];

            // Define the center point for all circles (in inches)
            double centerX = 5.0;
            double centerY = 5.0;

            // Define the incremental radius step (in inches)
            double radiusStep = 0.2;

            // Draw 50 circles with increasing radii
            for (int i = 1; i <= 50; i++)
            {
                double radius = i * radiusStep;
                double diameter = radius * 2.0;

                // Calculate the lower‑left corner of the bounding rectangle
                double pinX = centerX - radius;
                double pinY = centerY - radius;

                // Draw the circle (ellipse with equal width and height)
                page.DrawEllipse(pinX, pinY, diameter, diameter);
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Explicitly set the target format
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Set a fallback font (optional)
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram with 50 circles saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}