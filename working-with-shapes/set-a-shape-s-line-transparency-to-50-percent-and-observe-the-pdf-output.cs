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

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page at position (2,2) inches
                // The AddShape method returns the shape's unique ID (long)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the Shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the line (stroke) transparency to 50 percent
                // LineColorTrans.Value expects a percentage (0‑100)
                shape.Line.LineColorTrans.Value = 50;

                // Prepare PDF save options (set a default font to avoid missing‑font issues)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF file
                diagram.Save("output.pdf", pdfOptions);

                // Inform the user that the file has been created
                Console.WriteLine("Diagram saved to output.pdf with line transparency set to 50%.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }