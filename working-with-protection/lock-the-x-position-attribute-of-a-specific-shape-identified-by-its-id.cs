using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Identify the page containing the shape (using the first page as example)
                Page page = diagram.Pages[0];

                // Specify the ID of the shape whose X‑position should be locked
                long targetShapeId = 12345; // <-- replace with the actual shape ID

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Lock the X‑position (horizontal movement) of the shape
                shape.Protection.LockMoveX.Value = BOOL.True;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }