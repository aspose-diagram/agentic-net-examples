using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram
                Diagram diagram = new Diagram();

                // Use the first page (default page is created automatically)
                Page page = diagram.Pages[0];

                // Add a rectangle shape at position (2, 2) inches
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set solid fill pattern
                shape.Fill.FillPattern.Value = 1; // 1 = solid

                // Apply teal color (RGB 0,128,128) using hex notation
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