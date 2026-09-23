using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (index 0) of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape; AddShape returns the shape's ID (long)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set a double‑click event to call a VBA macro named "LogShapeID"
            // (EventMouseLeave does not exist; using a valid event cell instead)
            shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"LogShapeID\")";

            // Store the shape ID in a user‑defined cell so the macro can read it
            User user = new User();
            user.Name = "ShapeID";
            user.Value.Val = shapeId.ToString();
            shape.Users.Add(user);

            // Save the diagram to a VSDX file
            diagram.Save("EventMouseLeaveDemo.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}