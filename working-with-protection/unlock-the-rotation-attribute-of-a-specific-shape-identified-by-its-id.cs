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

                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // ID of the shape whose rotation lock should be removed
                long shapeId = 123; // <-- replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the shape is on the first page; adjust if necessary
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Unlock the rotation attribute (allow the shape to be rotated)
                shape.Protection.LockRotate.Value = BOOL.False;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Rotation lock removed and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }