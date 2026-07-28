using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // ID of the shape whose rotation lock should be removed
                long targetShapeId = 12345; // replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the shape by ID from the first page
                Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);
                if (shape == null)
                {
                    throw new Exception($"Shape with ID {targetShapeId} not found.");
                }

                // Unlock the rotation attribute
                shape.Protection.LockRotate.Value = BOOL.False;

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }