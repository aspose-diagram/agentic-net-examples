using System.IO;
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

            // IDs of the shapes that need to be rotated
            long[] shapeIds = new long[] { 5, 12, 20 };

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            foreach (long id in shapeIds)
            {
                // Retrieve the shape by its ID
                Shape shape = page.Shapes.GetShape(id);
                if (shape == null)
                {
                    Console.WriteLine($"Shape with ID {id} not found.");
                    continue;
                }

                // Rotate the shape by adding 45 degrees to its current angle
                double currentAngle = shape.XForm.Angle.Value;
                double newAngle = currentAngle + 45.0;
                shape.XForm.Angle.Value = newAngle;

                Console.WriteLine($"Rotated shape ID {id} from {currentAngle}° to {newAngle}°.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
