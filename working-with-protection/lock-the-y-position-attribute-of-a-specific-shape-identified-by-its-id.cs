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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Specify the shape ID to lock (replace with actual ID)
                long targetShapeId = 5; // example ID

                // Retrieve the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(targetShapeId);

                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Lock the Y‑position (vertical movement) of the shape
                shape.Protection.LockMoveY.Value = BOOL.True;

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