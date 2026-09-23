using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Instantiate a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the first page.
            // The fourth parameter expects an int (0 = false, 1 = true) in this API version.
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Create a field, assign a static string value, and attach it to the shape.
            Field field = new Field();
            field.Value.Val = "Custom static text";
            shape.Fields.Add(field);

            // Save the diagram as VSDX.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}