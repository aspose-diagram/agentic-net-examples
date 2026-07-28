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

                // ID of the shape whose height lock should be removed
                long shapeId = 123; // TODO: replace with the actual shape ID

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Get the shape by its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Unlock the height attribute by setting the lock to FALSE
                shape.Protection.LockHeight.Value = BOOL.False;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }