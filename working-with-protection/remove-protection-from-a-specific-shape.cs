using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (using the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Identify the shape to modify (replace with the actual shape ID)
            int shapeId = 5; // example shape ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Remove protection flags from the shape
            shape.Protection.LockDelete.Value = 0;
            shape.Protection.LockMoveX.Value = 0;
            shape.Protection.LockMoveY.Value = 0;
            shape.Protection.LockSelect.Value = 0;
            shape.Protection.LockFormat.Value = 0;
            shape.Protection.LockTextEdit.Value = 0;

            // Save the updated diagram (using the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
