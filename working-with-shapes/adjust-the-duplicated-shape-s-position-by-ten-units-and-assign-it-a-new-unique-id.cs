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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page
            Page page = diagram.Pages[0];

            // ID of the shape to duplicate (example: 1)
            long originalShapeId = 1;

            // Retrieve the original shape
            Shape originalShape = page.Shapes.GetShape(originalShapeId);
            if (originalShape == null)
            {
                Console.WriteLine($"Shape with ID {originalShapeId} not found.");
                return;
            }

            // Ensure the shape has an associated master
            if (originalShape.Master == null)
            {
                Console.WriteLine("The original shape does not have a master to duplicate from.");
                return;
            }

            // Get the master name to use for the new shape
            string masterName = originalShape.Master.Name;

            // Capture original position
            double origPinX = originalShape.XForm.PinX.Value;
            double origPinY = originalShape.XForm.PinY.Value;

            // Add a new shape based on the same master, shifted by 10 units on both axes
            long newShapeId = page.AddShape(origPinX + 10, origPinY + 10, masterName, false);

            // Retrieve the newly added shape
            Shape newShape = page.Shapes.GetShape(newShapeId);
            if (newShape == null)
            {
                Console.WriteLine("Failed to retrieve the newly created shape.");
                return;
            }

            // Explicitly set the new position (redundant but ensures correctness)
            newShape.XForm.PinX.Value = origPinX + 10;
            newShape.XForm.PinY.Value = origPinY + 10;

            // Assign a new unique ID (the ID returned by AddShape is already unique)
            // This line demonstrates explicit assignment if needed.
            newShape.ID = newShapeId;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Duplicated shape created with ID {newShapeId} at position ({newShape.XForm.PinX.Value}, {newShape.XForm.PinY.Value}).");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
