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

                // Add a rectangle shape to the active page
                // Parameters: PinX, PinY, master name ("Rectangle")
                long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Set line color to red so the transparency effect is visible
                shape.Line.LineColor.Value = "#FF0000";

                // Set line transparency to 50 percent (0 = opaque, 100 = fully transparent)
                shape.Line.LineColorTrans.Value = 50;

                // Optional: set line weight for better visibility
                shape.Line.LineWeight.Value = 0.02; // inches

                // Prepare PDF save options (default font required to avoid warnings)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF file
                string outputPath = "LineTransparencyDemo.pdf";
                diagram.Save(outputPath, pdfOptions);

                // Inform the user that the PDF has been generated
                Console.WriteLine($"PDF saved to '{outputPath}'. The shape's line transparency is set to 50%.");

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }