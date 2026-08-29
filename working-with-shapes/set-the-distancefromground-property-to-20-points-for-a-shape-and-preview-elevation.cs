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

            // Load an existing Visio file or create a new diagram
            Diagram diagram = new Diagram(); // creates an empty diagram

            // Ensure there is at least one page
            Page page = diagram.ActivePage;

            // Add a rectangle shape to the page (pinX, pinY, width, height, master name)
            long shapeId = page.AddShape(2.0, 2.0, 1.5, 1.0, "Rectangle");

            // Retrieve the concrete Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the distance from ground (elevation) to 20 points
            shape.ThreeDFormat.DistanceFromGround.Value = 20.0;

            // Refresh the shape so the change is reflected in the diagram view
            shape.RefreshData();

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
