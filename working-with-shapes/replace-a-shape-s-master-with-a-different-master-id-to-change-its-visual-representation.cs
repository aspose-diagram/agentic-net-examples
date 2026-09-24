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

            // Define the ID of the shape whose master we want to replace
            long targetShapeId = 1; // adjust as needed

            // Define the name of the new master to apply
            string newMasterName = "NewMaster"; // ensure this master exists in the diagram

            // Verify that the target shape exists
            Page page = diagram.Pages[0];
            Shape targetShape = page.Shapes.GetShape(targetShapeId);
            if (targetShape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found.");
            }

            // Verify that the new master exists
            if (!diagram.Masters.IsExist(newMasterName))
            {
                throw new Exception($"Master \"{newMasterName}\" does not exist in the diagram.");
            }

            // Preserve the original shape's geometry
            double pinX = targetShape.XForm.PinX.Value;
            double pinY = targetShape.XForm.PinY.Value;
            double width = targetShape.XForm.Width.Value;
            double height = targetShape.XForm.Height.Value;

            // Mark the original shape for deletion
            targetShape.Del = BOOL.True;

            // Add a new shape using the new master at the same position and size
            long newShapeId = page.AddShape(pinX, pinY, width, height, newMasterName, false);
            Shape newShape = page.Shapes.GetShape(newShapeId);

            // Optionally copy text from the old shape to the new one
            if (!string.IsNullOrWhiteSpace(targetShape.Text.Value.ToString()))
            {
                newShape.Text.Value.Clear();
                newShape.Text.Value.Add(new Txt(targetShape.Text.Value.ToString()));
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
