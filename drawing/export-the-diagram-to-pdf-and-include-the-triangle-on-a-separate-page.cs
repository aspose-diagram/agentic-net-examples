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

        // Add a new page that will contain the triangle
        Page trianglePage = new Page();
        diagram.Pages.Add(trianglePage);

        // Get the page we just added (last page in the collection)
        Page page = diagram.Pages[diagram.Pages.Count - 1];

        // Define triangle vertices (coordinates are in inches)
        double x1 = 2.0, y1 = 2.0;
        double x2 = 4.0, y2 = 2.0;
        double x3 = 3.0, y3 = 4.0;

        // Draw the triangle using a polyline; close the shape by repeating the first point
        page.DrawPolyline(new double[] { x1, y1, x2, y2, x3, y3, x1, y1 });

        // Configure PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";

        // Export the diagram (including the triangle page) to PDF
        string outputPath = "DiagramWithTriangle.pdf";
        diagram.Save(outputPath, pdfOptions);
    }
}
