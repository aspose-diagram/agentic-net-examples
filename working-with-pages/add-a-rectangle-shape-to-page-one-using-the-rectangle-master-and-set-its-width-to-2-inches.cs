using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty Visio diagram
                using (Diagram diagram = new Diagram())
                {
                    // Access the first page (page index 0)
                    Page page = diagram.Pages[0];

                    // Add a rectangle shape using the built‑in "Rectangle" master.
                    // PinX and PinY define the position of the shape's pin (center).
                    double pinX = 2.0; // inches from the left edge
                    double pinY = 2.0; // inches from the top edge
                    long shapeId = page.AddShape(pinX, pinY, "Rectangle");

                    // Retrieve the shape object by its ID
                    Shape rectangle = page.Shapes.GetShape(shapeId);

                    // Set the shape's width to 2 inches
                    rectangle.XForm.Width.Value = 2.0;

                    // (Optional) Save the diagram to a VSDX file
                    diagram.Save("RectangleDiagram.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }