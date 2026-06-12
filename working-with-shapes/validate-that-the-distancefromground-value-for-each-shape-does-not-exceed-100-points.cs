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

            bool allValid = true;

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a 3D format and a DistanceFromGround value
                    if (shape.ThreeDFormat != null && shape.ThreeDFormat.DistanceFromGround != null)
                    {
                        double distance = shape.ThreeDFormat.DistanceFromGround.Value;

                        // Validate the distance does not exceed 100 points
                        if (distance > 100)
                        {
                            allValid = false;
                            Console.WriteLine($"Shape ID {shape.ID} exceeds 100 points: DistanceFromGround = {distance}");
                        }
                    }
                }
            }

            if (allValid)
            {
                Console.WriteLine("All shapes have DistanceFromGround <= 100 points.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
