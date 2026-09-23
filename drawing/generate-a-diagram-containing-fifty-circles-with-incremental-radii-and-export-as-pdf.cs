using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first page of the diagram
        Page page = diagram.Pages[0];

        // Define circle parameters
        double startRadius = 0.5;          // initial radius in inches
        double radiusIncrement = 0.1;      // radius increase per circle
        double spacing = 0.5;              // extra horizontal spacing between circles

        // Add 50 circles with incremental radii
        for (int i = 0; i < 50; i++)
        {
            double radius = startRadius + i * radiusIncrement;
            double diameter = radius * 2;

            // Position each circle horizontally, keeping a constant vertical position
            double pinX = i * (diameter + spacing) + radius;
            double pinY = 5.0; // fixed Y coordinate

            // Draw a circle (ellipse with equal width and height)
            page.DrawEllipse(pinX, pinY, diameter, diameter);
        }

        // Configure PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";
        pdfOptions.SaveFormat = SaveFileFormat.Pdf;

        // Export the diagram to PDF
        diagram.Save("Circles.pdf", pdfOptions);
    }
}
