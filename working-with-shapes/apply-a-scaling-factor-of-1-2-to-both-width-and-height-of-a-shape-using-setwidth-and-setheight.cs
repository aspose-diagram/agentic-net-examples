using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram (contains a default page)
                Diagram diagram = new Diagram();

                // Add a rectangle shape to the first page at position (2,2)
                // The AddShape method returns the shape's unique ID (long)
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                // Retrieve the shape instance from the page's shape collection
                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(shapeId);

                // Read the current width and height (values are in inches)
                double currentWidth = shape.XForm.Width.Value;
                double currentHeight = shape.XForm.Height.Value;

                // Define the scaling factor
                double scaleFactor = 1.2;

                // Apply the scaling factor using SetWidth and SetHeight
                shape.SetWidth(currentWidth * scaleFactor);
                shape.SetHeight(currentHeight * scaleFactor);

                // Save the diagram to verify the changes (optional)
                diagram.Save("ScaledShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }