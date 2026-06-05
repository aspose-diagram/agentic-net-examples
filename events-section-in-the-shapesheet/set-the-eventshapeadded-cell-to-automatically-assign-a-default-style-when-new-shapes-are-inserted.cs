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

            // Add a new page (optional, the default diagram already has one page)
            // Page page = diagram.Pages[0];

            // Add a rectangle shape to the first page
            // Parameters: PinX, PinY, Width, Height, Master name
            long rectId = diagram.AddShape(5.0, 5.0, 2.0, 1.0, "Rectangle", 0);

            // Retrieve the shape instance to modify its event cells
            Shape rectShape = diagram.Pages[0].Shapes.GetShape(rectId);

            // Set the EventDrop cell to assign a default style when the shape is dropped/added.
            // The formula uses the SETSTYLE function to apply a style named "Basic".
            // Adjust the style name as needed for your diagram.
            rectShape.Event.EventDrop.Ufe.F = "SETSTYLE(\"Basic\")";

            // Save the diagram to a VSDX file
            diagram.Save("StyledDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
