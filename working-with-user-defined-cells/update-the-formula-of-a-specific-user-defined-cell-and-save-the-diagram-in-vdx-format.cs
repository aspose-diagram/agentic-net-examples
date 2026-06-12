using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination files
                string inputPath = "input.vsdx";   // replace with your source diagram path
                string outputPath = "output.vdx"; // destination VDX file

                // Identify the page, shape, and user-defined cell to modify
                int pageIndex = 0;          // zero‑based index of the page
                int shapeId = 1;            // ID of the shape containing the user cell
                string targetUserCellName = "MyUserCell"; // name of the user‑defined cell
                string newFormula = "Width*Height";       // new formula to assign

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the specified page
                Page page = diagram.Pages[pageIndex];

                // Retrieve the specified shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Find the user‑defined cell by name and update its formula
                bool cellFound = false;
                foreach (User userCell in shape.Users)
                {
                    if (userCell.Name == targetUserCellName || userCell.NameU == targetUserCellName)
                    {
                        userCell.Value.Val = newFormula;
                        cellFound = true;
                        break;
                    }
                }

                if (!cellFound)
                {
                    Console.WriteLine($"User‑defined cell \"{targetUserCellName}\" not found in shape ID {shapeId}.");
                    return;
                }

                // Save the modified diagram in VDX format using DiagramSaveOptions
                DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vdx);
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\" with updated user‑defined cell.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }