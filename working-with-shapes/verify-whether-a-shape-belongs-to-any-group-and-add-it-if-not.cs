using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // ID of the shape to check (replace with the actual ID)
            long shapeId = 5;

            // Load the diagram (lifecycle rule: use provided load method)
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its unique ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Verify whether the shape is already part of a group
            if (!shape.IsInGroup())
            {
                // Shape is not grouped; create a new group containing this shape
                // Group method returns the newly created group shape
                Shape groupShape = page.Shapes.Group(new Shape[] { shape });

                // Example: set the group's selection mode (optional)
                groupShape.Group.SelectMode.Value = SelectModeValue.GroupShapeOnly;
            }

            // Save the modified diagram (lifecycle rule: use provided save method)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
