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

            // Use the active page to add a rectangle shape
            Page page = diagram.ActivePage;
            // DrawRectangle(pinX, pinY, width, height) – all values are in inches
            long shapeId = page.DrawRectangle(2.0, 2.0, 4.0, 2.0);

            // Retrieve the Shape object from the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a new Field object
            Field field = new Field();

            // Assign a static string value to the field (this will appear as custom text on the shape)
            field.Value.Val = "Custom Text";

            // Add the field to the shape's Fields collection
            shape.Fields.Add(field);

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
