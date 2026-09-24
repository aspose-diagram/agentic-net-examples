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

            // Select the page that contains the shapes (using the first page as an example)
            Page page = diagram.Pages[0];

            // Define the IDs of the shapes to rotate
            long[] shapeIds = new long[] { 1, 2, 3 };

            // Rotate each shape by 45 degrees
            foreach (long id in shapeIds)
            {
                // Retrieve the shape by its ID using GetShape
                Shape shape = page.Shapes.GetShape(id);

                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.False)
                {
                    // Add 45 degrees to the current rotation angle
                    double currentAngle = shape.XForm.Angle.Value;
                    shape.XForm.Angle.Value = currentAngle + 45.0;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
