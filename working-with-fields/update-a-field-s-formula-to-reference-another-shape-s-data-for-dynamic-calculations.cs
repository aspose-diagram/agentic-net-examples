using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // IDs or names of the shapes involved
            const string targetShapeNameU = "TargetShape"; // shape that contains the field to update
            const int sourceShapeId = 5; // shape whose Data1 cell will be referenced

            Shape targetShape = null;

            // Locate the target shape by its universal name
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeNameU)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Target shape \"{targetShapeNameU}\" not found.");
                return;
            }

            // Ensure the shape has at least one field to modify
            if (targetShape.Fields.Count == 0)
            {
                Console.WriteLine("Target shape does not contain any fields.");
                return;
            }

            // Update the first field's formula to reference the Data1 cell of the source shape
            Field field = targetShape.Fields[0];
            field.Value.Ufev.F = $"Sheet.{sourceShapeId}!Data1";

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with updated field formula.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
