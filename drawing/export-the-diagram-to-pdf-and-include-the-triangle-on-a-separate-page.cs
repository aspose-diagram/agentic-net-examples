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
        trianglePage.ID = diagram.Pages.Count + 1; // ensure a unique page ID
        diagram.Pages.Add(trianglePage);

        // Define the triangle vertices (PinX, PinY) in inches.
        // The first point is repeated at the end to close the shape.
        double[] trianglePoints = new double[] { 2, 2, 5, 2, 3.5, 5, 2, 2 };

        // Draw the triangle on the newly created page.
        long triangleShapeId = trianglePage.DrawPolyline(trianglePoints);

        // Add a label to the triangle (optional).
        Shape triangleShape = trianglePage.Shapes.GetShape((int)triangleShapeId);
        triangleShape.Text.Value.Clear();
        triangleShape.Text.Value.Add(new Txt("Triangle"));

        // Configure PDF save options.
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";

        // Export the diagram to PDF. Both the original (empty) page and the
        // page with the triangle will be included in the output file.
        diagram.Save("DiagramWithTriangle.pdf", pdfOptions);
    }
}
