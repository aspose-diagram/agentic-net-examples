using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (master name "Rectangle")
            // The shape is added to the diagram and returns a long ID
            long shapeIdLong = diagram.AddShape(1.0, 1.0, "Rectangle", 0);

            // Convert the long ID to int as required by GetShape
            Shape shape = page.Shapes.GetShape((int)shapeIdLong);

            // Create a user‑defined cell (custom property)
            User userCell = new User();
            userCell.Name = "MyCustomProp";
            userCell.Value.Val = "12345";

            // Add the custom property to the shape
            shape.Users.Add(userCell);

            // Lock custom properties to make the user‑defined cell read‑only at runtime
            shape.Protection.LockCustProp.Value = BOOL.True;

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
