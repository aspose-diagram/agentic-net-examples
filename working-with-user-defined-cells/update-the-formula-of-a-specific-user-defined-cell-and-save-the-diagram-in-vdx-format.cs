using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output files
            string inputPath = "input.vsdx";
            string outputPath = "output.vdx";

            // Load the diagram (assuming VSDX input)
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Identify the target shape (example: shape with ID 1 on the first page)
            int targetShapeId = 1;
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Name of the user‑defined cell to update
            string userCellName = "MyCell";

            // Locate the user‑defined cell and update its formula
            bool cellFound = false;
            foreach (User user in shape.Users)
            {
                if (user.Name == userCellName || user.NameU == userCellName)
                {
                    // Set the new formula (as a string)
                    user.Value.Val = "Width*Height";
                    cellFound = true;
                    Console.WriteLine($"User cell '{userCellName}' formula updated.");
                    break;
                }
            }

            if (!cellFound)
            {
                Console.WriteLine($"User cell '{userCellName}' not found in shape ID {targetShapeId}.");
            }

            // Save the modified diagram in VDX format
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
