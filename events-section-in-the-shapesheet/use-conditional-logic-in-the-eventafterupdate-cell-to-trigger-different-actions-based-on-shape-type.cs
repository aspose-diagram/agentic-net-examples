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

            // Add a rectangle shape on the active page
            long rectId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");
            Shape rectShape = diagram.ActivePage.Shapes.GetShape(rectId);

            // Add an ellipse shape on the active page
            long ellipseId = diagram.ActivePage.AddShape(4.0, 2.0, "Ellipse");
            Shape ellipseShape = diagram.ActivePage.Shapes.GetShape(ellipseId);

            // Set an event formula that uses conditional logic.
            // The formula checks the master name of the shape and calls a different macro accordingly.
            // This is placed in the EventDrop cell (a valid event cell) using the required Ufe.F property.
            string conditionalFormula = "IF(Master.Name=\"Rectangle\", CALLTHIS(\"RectAction\"), CALLTHIS(\"OtherAction\"))";

            // Apply the same conditional formula to both shapes.
            // At runtime Visio will evaluate the condition based on each shape's master.
            rectShape.Event.EventDrop.Ufe.F = conditionalFormula;
            ellipseShape.Event.EventDrop.Ufe.F = conditionalFormula;

            // Save the diagram to a VSDX file
            diagram.Save("ConditionalEventDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
