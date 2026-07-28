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

            // Add a rectangle shape at coordinates (2, 2) inches
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle", false);
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Create a field that will display a dynamic value (e.g., Width * Height)
            Field field = new Field();

            // Set the field type to a custom formula (Undefined)
            field.Type.Value = TypeFieldValue.Undefined;

            // Define the formula for the field
            field.Value.Ufev.F = "Width*Height";
            field.Value.Ufev.Unit = MeasureConst.Undefined;

            // Ensure no static text overrides the formula
            field.Value.Val = "";

            // Add the field to the shape's field collection
            shape.Fields.Add(field);

            // Save the diagram to a VSDX file
            diagram.Save("DynamicFieldDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
