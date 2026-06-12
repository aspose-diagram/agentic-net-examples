using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve a shape (for example, the shape with ID 1 on the first page)
            long shapeId = 1;
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Define the new line weight value
            double newLineWeight = 3.0;

            // Apply the new line weight to the shape's line
            shape.Line.LineWeight = new DoubleValue { Value = newLineWeight };

            // Verify that the line weight was updated
            double updatedWeight = shape.Line.LineWeight.Value;
            Console.WriteLine($"Updated line weight: {updatedWeight}");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
