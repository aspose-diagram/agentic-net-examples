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

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate a group shape on the page (first one found)
            Shape groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            if (groupShape == null)
            {
                throw new Exception("No group shape found on the page.");
            }

            // Enable dynamic gluing for the group shape
            groupShape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

            // Add a shared connector (Dynamic connector) to the page
            long connectorId = page.AddShape(2.0, 2.0, "Dynamic connector", false);
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect the group shape to the shared connector
            page.ConnectShapesViaConnector(
                groupShape.ID,
                ConnectionPointPlace.Bottom,
                connectorId,
                ConnectionPointPlace.Top,
                connectorId);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
