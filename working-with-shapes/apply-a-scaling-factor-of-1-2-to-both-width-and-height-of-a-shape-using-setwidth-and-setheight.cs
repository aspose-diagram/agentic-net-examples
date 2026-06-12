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

                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page
                // Parameters: pinX, pinY, width, height, master name
                long shapeId = page.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle");

                // Retrieve the shape object using its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Apply a scaling factor of 1.2 to width and height
                double scaledWidth = shape.XForm.Width.Value * 1.2;
                double scaledHeight = shape.XForm.Height.Value * 1.2;

                // Use the SetWidth and SetHeight methods as required
                shape.SetWidth(scaledWidth);
                shape.SetHeight(scaledHeight);

                // Save the modified diagram
                diagram.Save("ScaledShapeOutput.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }