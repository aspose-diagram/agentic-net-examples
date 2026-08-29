using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has a ThreeDFormat and a RotationYAngle defined
                    if (shape.ThreeDFormat != null && shape.ThreeDFormat.RotationYAngle != null)
                    {
                        double angle = shape.ThreeDFormat.RotationYAngle.Value;
                        // Validate that the angle does not exceed 360 degrees
                        if (angle > 360.0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' exceeds 360° (Current: {angle}°).");
                            // Optionally, correct the value
                            shape.ThreeDFormat.RotationYAngle.Value = 360.0;
                        }
                    }
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
