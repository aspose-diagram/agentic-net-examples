using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the IDs of the shapes to rotate
                long[] shapeIds = { 5, 12, 23 }; // example IDs

                // Target rotation angle in radians (e.g., 90 degrees)
                double rotationAngle = Math.PI / 2;

                // Retrieve each shape by ID and apply the rotation
                foreach (long id in shapeIds)
                {
                    // Get the shape from the first page (index 0)
                    Shape shape = diagram.Pages[0].Shapes.GetShape(id);

                    // Ensure the shape exists and is not marked as deleted
                    if (shape != null && shape.Del == BOOL.False)
                    {
                        // Set the rotation angle
                        shape.XForm.Angle.Value = rotationAngle;
                    }
                    else
                    {
                        Console.WriteLine($"Shape with ID {id} not found or is deleted.");
                    }
                }

                // Save the modified diagram
                string outputPath = "RotatedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Batch rotation completed and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }