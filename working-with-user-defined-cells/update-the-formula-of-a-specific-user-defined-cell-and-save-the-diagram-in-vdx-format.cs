using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output files
            string inputPath = "input.vsdx";
            string outputPath = "output.vdx";

            // Load the diagram (assuming the source is a VSDX file)
            Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

            // Identify the shape and the user‑defined cell to modify
            int targetShapeId = 1;               // replace with the actual shape ID
            string userCellName = "MyUserCell";  // replace with the actual user cell name

            // Access the first page (adjust if the shape resides on another page)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(targetShapeId);

            // Try to find the existing user‑defined cell
            bool cellFound = false;
            foreach (User user in shape.Users)
            {
                if (user.Name == userCellName || user.NameU == userCellName)
                {
                    // Update the formula/value of the user‑defined cell
                    user.Value.Val = "Width*Height"; // example formula
                    cellFound = true;
                    break;
                }
            }

            // If the cell does not exist, create it and set the formula
            if (!cellFound)
            {
                User newUser = new User();
                newUser.Name = userCellName;
                newUser.Value.Val = "Width*Height"; // example formula
                shape.Users.Add(newUser);
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
