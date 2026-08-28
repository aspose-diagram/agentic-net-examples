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

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at position (2,2) on the page
            // The fourth parameter (isCalculate) must be a boolean
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the EventDrop cell to assign a default style ("Normal") when the shape is added/dropped
            // The formula is a valid Visio formula that runs on the drop event
            shape.Event.EventDrop.Ufe.F = "SETSTYLE(\"Normal\")";

            // Save the diagram to a VSDX file
            diagram.Save("EventShapeAddedDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
