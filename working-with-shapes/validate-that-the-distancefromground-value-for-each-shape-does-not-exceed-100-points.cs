using System.IO;
using System;
using Aspose.Diagram;

class ValidateDistanceFromGround
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has 3D format information
                    if (shape.ThreeDFormat != null && shape.ThreeDFormat.DistanceFromGround != null)
                    {
                        // Retrieve the distance value (in points)
                        double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                        // Validate that the distance does not exceed 100 points
                        if (distance > 100)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' exceeds the limit: DistanceFromGround = {distance} points.");
                        }
                    }
                }
            }

            Console.WriteLine("Validation completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
