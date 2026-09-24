using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_rotated.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired rotation angle in degrees
            double rotationAngle = 45.0;

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Example: rotate the shape with ID 1
            // Retrieve the shape; GetShape returns null if the ID does not exist
            Shape shape = page.Shapes.GetShape(1);
            if (shape != null)
            {
                // Set the rotation angle (degrees) via the XForm.Angle cell
                shape.XForm.Angle.Value = rotationAngle;
                Console.WriteLine($"Shape ID {shape.ID} rotated to {rotationAngle} degrees.");
            }
            else
            {
                Console.WriteLine("Shape with ID 1 not found.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
