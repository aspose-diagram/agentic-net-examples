using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (lifecycle rule: load)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page and a shape on it (replace 1 with the actual shape ID)
            Shape shape = diagram.Pages[0].Shapes.GetShape(1);

            // Rotate the text inside the shape by 45 degrees (diagonal effect)
            shape.TextXForm.TxtAngle.Value = 45;

            // Adjust text margins so the rotated text stays inside the shape boundaries
            shape.TextBlock.LeftMargin.Value   = 2; // points
            shape.TextBlock.TopMargin.Value    = 2;
            shape.TextBlock.RightMargin.Value  = 2;
            shape.TextBlock.BottomMargin.Value = 2;

            // Refresh shape data after modifying text properties (lifecycle rule: refresh)
            shape.RefreshData();

            // Save the modified diagram (lifecycle rule: save)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
