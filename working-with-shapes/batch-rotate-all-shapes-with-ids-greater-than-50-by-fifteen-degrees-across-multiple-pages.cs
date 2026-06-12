using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Path for the rotated output file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Rotation increment (15 degrees in radians)
            double rotationIncrement = Math.PI / 180.0 * 15.0;

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes with ID greater than 50
                    if (shape.ID > 50)
                    {
                        // Get current angle (in radians)
                        double currentAngle = shape.XForm.Angle.Value;

                        // Apply the additional rotation
                        shape.XForm.Angle.Value = currentAngle + rotationIncrement;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
