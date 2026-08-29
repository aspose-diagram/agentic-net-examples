using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path where the modified Visio file will be saved
            string outputPath = "output.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the target page (using the first page as an example)
            Page page = diagram.Pages[0];

            // IDs of the shapes that need to be grouped.
            // Replace these placeholder IDs with the actual shape IDs you want to group.
            long[] shapeIds = new long[] { 1, 2, 3 };

            // Collect the Shape objects corresponding to the IDs
            Shape[] shapesToGroup = new Shape[shapeIds.Length];
            for (int i = 0; i < shapeIds.Length; i++)
            {
                shapesToGroup[i] = page.Shapes.GetShape(shapeIds[i]);
            }

            // Create a group from the selected shapes
            Shape groupShape = page.Shapes.Group(shapesToGroup);

            // Optionally assign a name to the new group shape
            groupShape.Name = "MyGroup";

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
