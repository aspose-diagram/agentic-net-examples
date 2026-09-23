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

            // Path to a stencil file that contains the required masters (e.g., "Rectangle" and "Dynamic connector")
            string stencilPath = @"C:\Stencils\Basic_U.vss";
            // Output Visio file
            string outputPath = @"C:\Output\ConnectorDemo.vsdx";

            // Load the stencil as a diagram (masters become available)
            Diagram diagram = new Diagram(stencilPath);

            // Use the first page of the diagram
            Page page = diagram.Pages[0];

            // Add two rectangle shapes
            long shapeId1 = page.AddShape(2.0, 2.0, "Rectangle", false);
            long shapeId2 = page.AddShape(5.0, 5.0, "Rectangle", false);

            // Add a dynamic connector shape (initial position is irrelevant; it will be glued)
            long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector", false);

            // Connect the two rectangles using the connector
            page.ConnectShapesViaConnector(
                shapeId1,
                ConnectionPointPlace.Bottom,
                shapeId2,
                ConnectionPointPlace.Top,
                connectorId);

            // Retrieve the connector shape to modify its line style
            Shape connector = page.Shapes.GetShape(connectorId);
            // Set the line pattern to a dashed style
            connector.Line.LinePattern.Value = LinePatternValue.Dash;

            // Save the diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved to: " + outputPath);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
