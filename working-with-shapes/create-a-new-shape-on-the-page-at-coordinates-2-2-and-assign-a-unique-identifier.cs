using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram instance
            Diagram diagram = new Diagram();

            // Ensure the diagram has at least one page; add a blank page if none exist
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Retrieve the first page for shape operations
            Page page = diagram.Pages[0];

            // Draw a rectangle at (2,2) with width and height of 1 inch; returns the shape's ID (long)
            long shapeId = page.DrawRectangle(2.0, 2.0, 1.0, 1.0);

            // Get the Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Assign a unique identifier (GUID) directly to the shape's UniqueID property
            shape.UniqueID = Guid.NewGuid();

            // Define the output file path
            string outputPath = "CreatedShape.vsdx";

            // Save the diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}