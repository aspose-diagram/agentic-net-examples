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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // ID of the shape whose height attribute should be unlocked
            long shapeId = 5; // replace with the actual shape ID

            // Retrieve the shape from the first page (adjust page index if needed)
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {shapeId} not found.");
            }

            // Unlock the height attribute by disabling the height lock protection
            shape.Protection.LockHeight.Value = BOOL.False;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
