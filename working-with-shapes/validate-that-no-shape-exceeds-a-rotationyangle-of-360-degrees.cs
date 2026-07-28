using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a ThreeDFormat and a RotationYAngle defined
                    if (shape.ThreeDFormat != null && shape.ThreeDFormat.RotationYAngle != null)
                    {
                        double angle = shape.ThreeDFormat.RotationYAngle.Value;

                        // Validate that the RotationYAngle does not exceed 360 degrees
                        if (angle > 360.0)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' exceeds RotationYAngle: {angle} degrees.");
                        }
                    }
                }
            }

            // Save the diagram (unchanged) if needed
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
