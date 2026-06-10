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
                string sourcePath = "input.vsdx";

                // The ID of the shape whose rotation should be locked
                long shapeId = 123; // replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Assume the shape is on the first page; adjust if necessary
                Page page = diagram.Pages[0];

                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Lock the rotation attribute
                shape.Protection.LockRotate.Value = BOOL.True;

                // Save the modified diagram
                string outputPath = "output_locked.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Rotation locked for shape ID {shapeId}. Saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }