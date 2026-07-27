using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            Diagram diagram = new Diagram("TemplateDiagram.vsdx");

            // Identify the page that contains the template shape
            Page page = diagram.Pages[0]; // adjust index if needed

            // Get the template shape by its ID (replace with actual ID)
            long templateShapeId = 1; // <-- set the ID of the shape that holds the Event formulas
            Shape templateShape = page.Shapes.GetShape(templateShapeId);

            // Define master name to be used for new shapes (must exist in the document)
            string masterName = "Rectangle"; // <-- replace with your master name

            // Example: create 10 new shapes in a grid and copy Event formulas from the template
            int rows = 2;
            int cols = 5;
            double startX = 1.0;   // inches
            double startY = 1.0;   // inches
            double deltaX = 2.0;   // horizontal spacing
            double deltaY = 2.0;   // vertical spacing

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    double pinX = startX + c * deltaX;
                    double pinY = startY + r * deltaY;

                    // Add a new shape based on the specified master
                    long newShapeId = page.AddShape(pinX, pinY, masterName);
                    Shape newShape = page.Shapes.GetShape(newShapeId);

                    // Copy all Event formulas from the template shape to the new shape
                    // (Event properties are strings representing the formulas)
                    newShape.Event.EventDblClick = templateShape.Event.EventDblClick;
                    newShape.Event.EventDrop = templateShape.Event.EventDrop;
                    newShape.Event.EventMultiDrop = templateShape.Event.EventMultiDrop;
                    newShape.Event.EventXFMod = templateShape.Event.EventXFMod;
                    newShape.Event.TheText = templateShape.Event.TheText;

                    // Refresh shape data to ensure the new shape reflects any changes
                    newShape.RefreshData();
                }
            }

            // Save the modified diagram
            diagram.Save("ResultDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
