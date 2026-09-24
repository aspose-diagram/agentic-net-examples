using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new blank diagram
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Add a rectangle shape at coordinates (2,2)
                // The fourth parameter 'false' indicates that the shape's geometry should not be recalculated automatically
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Output the unique identifier assigned by Aspose.Diagram
                Console.WriteLine($"Created shape with ID: {shape.ID}");

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
