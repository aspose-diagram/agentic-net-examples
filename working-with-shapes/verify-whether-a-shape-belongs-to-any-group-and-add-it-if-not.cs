using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (use the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index as needed)
            Page page = diagram.Pages[0];

            // Identify the shape to check (replace with actual shape ID or retrieval logic)
            long targetShapeId = 1; // example ID
            Shape targetShape = page.Shapes.GetShape(targetShapeId);

            // Verify whether the shape is already part of a group
            if (!targetShape.IsInGroup())
            {
                // The shape is not grouped; create a new group containing this shape
                Shape groupShape = page.Shapes.Group(new Shape[] { targetShape });

                // Optional: configure group selection behavior
                groupShape.Group.SelectMode.Value = SelectModeValue.GroupShapeOnly;
            }

            // Save the modified diagram (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
