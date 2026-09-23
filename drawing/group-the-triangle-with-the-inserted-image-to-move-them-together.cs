using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            Shape triangleShape = null;
            Shape imageShape = null;

            // Locate the triangle shape (by master name) and the inserted image (foreign type)
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Identify triangle shape
                if (triangleShape == null && shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    continue;
                }

                // Identify image shape (foreign type)
                if (imageShape == null && shape.Type == TypeValue.Foreign)
                {
                    imageShape = shape;
                    continue;
                }

                // Stop when both are found
                if (triangleShape != null && imageShape != null)
                    break;
            }

            if (triangleShape == null || imageShape == null)
            {
                throw new Exception("Triangle shape or image shape not found in the diagram.");
            }

            // Group the triangle and the image together
            Shape groupShape = page.Shapes.Group(new Shape[] { triangleShape, imageShape });

            // Move the group (e.g., 2 inches right and 1 inch up)
            groupShape.Move(2.0, -1.0);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
