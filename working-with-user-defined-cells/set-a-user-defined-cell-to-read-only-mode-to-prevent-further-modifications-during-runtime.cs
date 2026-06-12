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

            // Load an existing Visio diagram (or create a new one if the file does not exist)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Use the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (PinX=2, PinY=2)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a user‑defined cell (custom property)
            User userCell = new User();
            userCell.Name = "MyReadOnlyCell";
            userCell.Value.Val = "123";
            shape.Users.Add(userCell);

            // Lock custom properties to make the user‑defined cell read‑only at runtime
            shape.Protection.LockCustProp.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
