using System.IO;
using System;
using Aspose.Diagram;

class ShapeIdConsistencyValidator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume the shape we want to rename is on the first page and has a known name
            const string originalShapeName = "OldShapeName";
            const string newShapeName = "NewShapeName";

            // Retrieve the shape by its original name
            Shape originalShape = diagram.Pages[0].Shapes.GetShape(originalShapeName);
            if (originalShape == null)
            {
                Console.WriteLine($"Shape with name '{originalShapeName}' not found.");
                return;
            }

            // Store the original ID
            long originalId = originalShape.ID;

            // Rename the shape
            originalShape.Name = newShapeName;

            // Refresh shape data to ensure internal references are updated
            originalShape.RefreshData();

            // Retrieve the shape by its new name
            Shape renamedShape = diagram.Pages[0].Shapes.GetShape(newShapeName);
            if (renamedShape == null)
            {
                Console.WriteLine($"Renamed shape with name '{newShapeName}' not found.");
                return;
            }

            // Validate that the ID has remained the same
            if (renamedShape.ID == originalId)
            {
                Console.WriteLine("Success: Shape ID remained consistent after renaming.");
            }
            else
            {
                Console.WriteLine($"Failure: Shape ID changed from {originalId} to {renamedShape.ID} after renaming.");
            }

            // Save the modified diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
