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

                // Insert a rectangle shape at coordinates (2,2)
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the fill pattern to solid (value 1)
                shape.Fill.FillPattern.Value = 1;

                // Apply a teal fill color using its RGB hex code
                shape.Fill.FillForegnd.Value = "#008080";

                // Save the diagram to a VSDX file
                diagram.Save("TealShape.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }