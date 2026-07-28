using System.IO;
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

            // Add a new page to the diagram
            diagram.Pages.Add(new Page());

            // Reference the first page (index 0)
            Page page = diagram.Pages[0];

            // Add a shape using a master name (e.g., "Rectangle")
            // The AddShape method returns the automatically assigned shape ID (long)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Output the ID for verification or further processing
            Console.WriteLine($"New shape ID: {shapeId}");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
