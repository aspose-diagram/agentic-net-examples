using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Identify the target shape (by ID). Replace with the actual ID of the shape you want to inspect.
                long targetShapeId = 1; // example ID
                Shape targetShape = page.Shapes.GetShape(targetShapeId);

                // Retrieve IDs of all shapes glued to the target shape.
                // Using GluedShapesAll1D to get all 1‑D connector shapes glued to the shape.
                long[] gluedShapeIds = targetShape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                // List the IDs of the glued shapes.
                Console.WriteLine($"Shapes glued to shape ID {targetShapeId}:");
                foreach (long id in gluedShapeIds)
                {
                    Console.WriteLine($"- Glued Shape ID: {id}");
                }

                // (Optional) Save the diagram unchanged if a save operation is required by the workflow.
                // string outputPath = "output.vsdx";
                // diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }