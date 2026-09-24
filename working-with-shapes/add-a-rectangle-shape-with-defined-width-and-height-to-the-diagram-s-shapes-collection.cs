using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Access the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Define rectangle dimensions (in inches) and position
                double pinX = 2.0;      // X coordinate of the shape's center
                double pinY = 2.0;      // Y coordinate of the shape's center
                double width = 3.0;     // Width of the rectangle
                double height = 2.0;    // Height of the rectangle

                // Add a rectangle shape using the master name "Rectangle"
                // The fourth parameter (isCalculate) must be a boolean
                long shapeId = page.AddShape(pinX, pinY, width, height, "Rectangle", false);

                // Retrieve the shape object from the Shapes collection
                Shape rectangle = page.Shapes.GetShape(shapeId);

                // (Optional) Verify that the shape was added with the correct size
                if (rectangle.XForm.Width.Value != width || rectangle.XForm.Height.Value != height)
                {
                    throw new Exception("Rectangle dimensions were not set correctly.");
                }

                // Save the diagram to a VSDX file
                diagram.Save("RectangleDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }