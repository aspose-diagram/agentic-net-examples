using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at coordinates (2,2) with a default size (1x1 inches)
            long shapeId = page.DrawRectangle(2.0, 2.0, 1.0, 1.0);

            // Retrieve the shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Assign a unique identifier (GUID) to the shape
            shape.UniqueID = Guid.NewGuid();

            // Output the shape's internal ID and the assigned UniqueID
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Unique Identifier: {shape.UniqueID}");

            // Optional: save the diagram to a file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}