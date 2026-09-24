using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Load masters for the required shapes from a stencil file
            // (Replace "stencil.vss" with the actual path to your stencil)
            string stencilPath = "stencil.vss";
            diagram.AddMaster(stencilPath, "Star");
            diagram.AddMaster(stencilPath, "Rectangle");
            diagram.AddMaster(stencilPath, "Dynamic connector");

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a star shape
            // Parameters: pinX, pinY, master name, page index (0 for the first page)
            long starShapeId = diagram.AddShape(2.0, 2.0, "Star", 0);

            // Add a rectangle shape
            long rectShapeId = diagram.AddShape(5.0, 2.0, "Rectangle", 0);

            // Add a dynamic connector shape
            Shape connectorShape = new Shape();
            long connectorShapeId = diagram.AddShape(connectorShape, "Dynamic connector", 0);

            // Connect the star to the rectangle using connection point indexes
            // Source index = 1, Target index = 2
            page.ConnectShapesViaConnectorIndex(starShapeId, 1, rectShapeId, 2, connectorShapeId);

            // Save the diagram to a VSDX file
            diagram.Save("ConnectedDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
