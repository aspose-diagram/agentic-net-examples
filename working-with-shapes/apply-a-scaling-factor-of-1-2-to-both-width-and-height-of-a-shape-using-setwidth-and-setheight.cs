using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (and only) page of the diagram
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page at position (2,2) inches
                // The AddShape method returns the shape's unique ID (long)
                long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a scaling factor of 1.2 to both width and height
                double scaleFactor = 1.2;

                // Current dimensions are stored in the XForm cell collection
                double currentWidth = shape.XForm.Width.Value;
                double currentHeight = shape.XForm.Height.Value;

                // Set the new dimensions using the SetWidth and SetHeight methods
                shape.SetWidth(currentWidth * scaleFactor);
                shape.SetHeight(currentHeight * scaleFactor);

                // Save the modified diagram to a VSDX file
                diagram.Save("scaled.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }