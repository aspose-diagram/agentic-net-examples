using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram inside a try/catch to capture Aspose errors.
        try
        {
            Diagram diagram = new Diagram();

            // Retrieve the first page (the diagram always contains at least one page).
            Page page = diagram.Pages[0];

            // Add two rectangle shapes that will serve as the connector's endpoints.
            // AddShape returns a shape ID (long), so we store the ID and then fetch the Shape object.
            long shape1Id = page.AddShape(2.0, 5.0, "Rectangle");
            Shape shape1 = page.Shapes.GetShape(shape1Id);

            long shape2Id = page.AddShape(8.0, 5.0, "Rectangle");
            Shape shape2 = page.Shapes.GetShape(shape2Id);

            // Add a dynamic connector shape; again retrieve the Shape instance via its ID.
            long connectorId = page.AddShape(5.0, 5.0, "Dynamic connector");
            Shape connector = page.Shapes.GetShape(connectorId);

            // Connect shape1 to shape2 using the connector.
            // The ConnectionPointPlace enum specifies the exact segment (bottom of shape1, top of shape2).
            page.ConnectShapesViaConnector(
                shape1Id,
                ConnectionPointPlace.Bottom,
                shape2Id,
                ConnectionPointPlace.Top,
                connectorId);

            // Determine a point on the connector where the comment should appear.
            // Here we use the connector's own PinX and PinY (its centre) as the comment location.
            double commentX = connector.XForm.PinX.Value;
            double commentY = connector.XForm.PinY.Value;

            // Attach a comment to the connector at the calculated coordinates.
            page.AddComment(commentX, commentY, "This is a comment on the connector segment.");

            // Define the output file path.
            string outputPath = "ConnectorWithComment.vsdx";

            // Save the diagram using the correct overload (path + SaveFileFormat).
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}