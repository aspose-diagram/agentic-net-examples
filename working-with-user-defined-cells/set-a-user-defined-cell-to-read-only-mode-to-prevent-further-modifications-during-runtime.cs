using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape on the first page (page index 0)
            double pinX = 2.0;
            double pinY = 2.0;
            string masterName = "Rectangle";
            int pageIndex = 0;
            long shapeId = diagram.AddShape(pinX, pinY, masterName, pageIndex);

            // Retrieve the shape object from the page
            Shape shape = diagram.Pages[pageIndex].Shapes.GetShape((int)shapeId);

            // Create a user‑defined cell (custom property) and add it to the shape
            User userCell = new User();
            userCell.Name = "MyReadOnlyCell";
            userCell.Value.Val = "123";
            shape.Users.Add(userCell);

            // Lock custom properties to make the user‑defined cell read‑only at runtime
            shape.Protection.LockCustProp.Value = BOOL.True;

            // Save the diagram to a VSDX file
            diagram.Save("ReadOnlyCellDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
