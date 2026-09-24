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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Identify the page and shape that contains the user‑defined cell
            // Here we assume the shape is on the first page and has ID 1
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // shape ID = 1

            // Name of the user‑defined cell to update
            string targetUserCellName = "MyCell";

            // New formula to assign to the user‑defined cell
            string newFormula = "Width*Height";

            // Locate the user‑defined cell and update its formula
            foreach (User userCell in shape.Users)
            {
                if (userCell.Name == targetUserCellName)
                {
                    userCell.Value.Val = newFormula;
                    break;
                }
            }

            // Save the modified diagram in VDX format
            string outputPath = "output.vdx";
            diagram.Save(outputPath, SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
