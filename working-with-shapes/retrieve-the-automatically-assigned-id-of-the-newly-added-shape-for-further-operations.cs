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

            // Add a rectangle shape to the diagram.
            // The AddShape method returns the automatically assigned shape ID (type long).
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the Shape object using the returned ID.
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Output the shape ID for verification or further processing.
            Console.WriteLine($"New shape ID: {shapeId}");

            // Example of a further operation: set the shape's text.
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Hello Aspose.Diagram"));

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
