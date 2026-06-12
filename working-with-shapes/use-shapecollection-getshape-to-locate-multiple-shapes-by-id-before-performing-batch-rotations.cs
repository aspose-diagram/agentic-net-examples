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

            // Select the first page (index 0)
            Page page = diagram.Pages[0];

            // IDs of the shapes we want to rotate
            long[] shapeIds = { 5, 10, 15 };

            // Rotate each shape by 45 degrees (using SetAngle as per the rule set)
            foreach (long id in shapeIds)
            {
                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(id);

                // Ensure the shape exists and is not marked as deleted
                if (shape != null && shape.Del == BOOL.False)
                {
                    shape.SetAngle(45); // Rotate 45 degrees
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
