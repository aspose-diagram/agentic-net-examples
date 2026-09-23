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

            // Add a rectangle shape to the active page
            double pinX = 2.0;
            double pinY = 2.0;
            string masterName = "Rectangle";
            long shapeId = diagram.ActivePage.AddShape(pinX, pinY, masterName, false);
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Create a field that displays the current system date/time
            Field dateField = new Field();

            // Use the Visio formula NOW() to get the current timestamp
            dateField.Value.Ufev.F = "NOW()";

            // Optional: set a display format (e.g., short date)
            dateField.Format.Val = "Short Date";

            // Add the field to the shape
            shape.Fields.Add(dateField);

            // Save the diagram with the date/time field
            diagram.Save("DateTimeFieldDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
