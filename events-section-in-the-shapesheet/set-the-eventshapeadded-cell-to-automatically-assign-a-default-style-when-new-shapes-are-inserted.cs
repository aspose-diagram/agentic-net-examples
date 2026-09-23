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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Add a page to the diagram (required before adding shapes)
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate (bool)
            long shapeId = page.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", false);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set an event formula that will be triggered when the shape is added.
            // Since Aspose.Diagram does not expose an EventShapeAdded cell,
            // we use the closest available event cell: EventDrop.
            // The formula calls a Visio macro (AssignDefaultStyle) that should
            // apply the desired default style to the shape.
            shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"AssignDefaultStyle\")";

            // Optionally, define a simple default style directly on the shape
            // (e.g., fill color). This ensures the shape has a style even if
            // the macro is not present.
            shape.Fill.FillForegnd.Value = "#FFCC00"; // Light orange fill

            // Save the diagram to a VSDX file
            diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
