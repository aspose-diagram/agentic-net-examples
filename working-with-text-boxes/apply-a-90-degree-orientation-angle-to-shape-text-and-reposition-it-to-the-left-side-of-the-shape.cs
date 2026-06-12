using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page and a specific shape (example: shape with ID 1)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Apply a 90‑degree rotation to the shape's text block
            shape.TextXForm.TxtAngle.Value = 90;

            // Reposition the text block to the left side of the shape
            // Setting TxtPinX to 0 aligns the text's rotation center with the shape's left edge
            shape.TextXForm.TxtPinX.Value = 0;

            // Refresh the shape to ensure the changes take effect
            shape.RefreshData();

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
