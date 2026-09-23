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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Access the first page
            Page page = diagram.Pages[0];

            // Add the first rectangle shape (the one that will contain the field)
            double pinX1 = 2.0;
            double pinY1 = 2.0;
            double width1 = 1.0;
            double height1 = 1.0;
            string masterName = "Rectangle";
            long shapeId1 = page.AddShape(pinX1, pinY1, width1, height1, masterName, false);
            Shape shape1 = page.Shapes.GetShape((int)shapeId1);

            // Add a second rectangle shape whose Height will be referenced
            double pinX2 = 5.0;
            double pinY2 = 2.0;
            double width2 = 1.0;
            double height2 = 2.0; // arbitrary height
            long shapeId2 = page.AddShape(pinX2, pinY2, width2, height2, masterName, false);
            Shape shape2 = page.Shapes.GetShape((int)shapeId2);

            // Give the reference shape a universal name for formula usage
            shape2.NameU = "ReferenceShape";

            // Create a new field on shape1
            Field field = new Field();

            // Set the formula to reference the Height cell of the second shape
            field.Value.Ufev.F = "Height of shape \"ReferenceShape\"";

            // Optional: set an initial display value
            field.Value.Val = "0";

            // Add the field to shape1
            shape1.Fields.Add(field);

            // Save the diagram
            diagram.Save("ProportionalScaling.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
