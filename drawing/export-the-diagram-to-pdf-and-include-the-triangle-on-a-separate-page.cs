using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // -------------------------------------------------
            // Add a new page that will contain the triangle
            // -------------------------------------------------
            // Determine the next available page ID
            int maxPageId = 0;
            foreach (Page existingPage in diagram.Pages)
            {
                if (existingPage.ID > maxPageId)
                    maxPageId = existingPage.ID;
            }

            // Create and configure the new page
            Page trianglePage = new Page();
            trianglePage.ID = maxPageId + 1;               // Unique page ID
            trianglePage.Name = "TrianglePage";            // Optional friendly name

            // Add the new page to the diagram
            diagram.Pages.Add(trianglePage);

            // -------------------------------------------------
            // Draw a triangle on the newly added page
            // -------------------------------------------------
            // Define triangle vertices (in inches) and close the shape by repeating the first point
            double[] trianglePoints = new double[]
            {
                2.0, 2.0,   // Vertex 1 (PinX, PinY)
                4.0, 2.0,   // Vertex 2
                3.0, 4.0,   // Vertex 3
                2.0, 2.0    // Close the polygon back to Vertex 1
            };

            // Draw the polyline (triangle) on the page
            // The method returns the shape ID (long), which we do not need further here
            trianglePage.DrawPolyline(trianglePoints);

            // -------------------------------------------------
            // Export the diagram to PDF
            // -------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";                     // Fallback font
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;           // Explicitly set format

            // Save the diagram; the triangle will appear on its own page
            diagram.Save("DiagramWithTriangle.pdf", pdfOptions);
        }
    }