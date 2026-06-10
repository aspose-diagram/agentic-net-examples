using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the output Visio file
                string outputPath = "output_locked.vsdx";

                // The ID of the shape whose Y‑position (PinY) should be locked
                long targetShapeId = 12345; // <-- replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the shape is on the first page; adjust if necessary
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found on page {page.Name}.");
                }

                // Lock vertical movement (Y‑position) of the shape
                shape.Protection.LockMoveY.Value = BOOL.True;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }