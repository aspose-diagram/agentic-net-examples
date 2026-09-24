using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to an existing Visio file (replace with a valid file path)
            string inputPath = "input.vsdx";
            // Path for the output file after modification
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape (assumes a shape with ID 1 exists)
            Shape shape = page.Shapes.GetShape(1);

            // Set the rotation angle to 45 degrees
            double expectedAngle = 45.0;
            shape.SetAngle(expectedAngle);

            // Read back the angle from the shape's Angle cell
            double actualAngle = shape.XForm.Angle.Value;

            // Verify that the angle was set correctly
            if (Math.Abs(actualAngle - expectedAngle) > 0.001)
            {
                throw new Exception($"Angle verification failed. Expected: {expectedAngle}, Actual: {actualAngle}");
            }
            else
            {
                Console.WriteLine($"Angle verification succeeded. Angle is {actualAngle} degrees.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
