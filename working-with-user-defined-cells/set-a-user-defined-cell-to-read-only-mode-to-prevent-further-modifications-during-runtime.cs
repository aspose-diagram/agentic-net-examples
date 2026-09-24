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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the diagram (position X=2, Y=2, master name "Rectangle", on page index 0)
            long shapeId = diagram.AddShape(2, 2, "Rectangle", 0);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Create a user‑defined cell (custom property)
            User userCell = new User();
            userCell.Name = "MyCustomCell";
            userCell.Value.Val = "123";
            shape.Users.Add(userCell);

            // Set the shape's protection to lock custom properties, making the user‑defined cell read‑only
            shape.Protection.LockCustProp.Value = BOOL.True;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
