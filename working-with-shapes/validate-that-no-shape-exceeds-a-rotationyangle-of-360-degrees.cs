using System.IO;
using Aspose.Diagram;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Collect information about shapes with invalid RotationYAngle
            List<string> invalidShapes = new List<string>();

            // Iterate through every page and shape in the diagram
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
                            // Record details of the offending shape
                            invalidShapes.Add($"Page: {page.Name}, Shape ID: {shape.ID}, RotationYAngle: {angle}");
                        }
                    }
                }
            }

            // Report validation results
            if (invalidShapes.Count == 0)
            {
                Console.WriteLine("All shapes have RotationYAngle within 0‑360 degrees.");
            }
            else
            {
                Console.WriteLine("Shapes exceeding RotationYAngle of 360 degrees:");
                foreach (string info in invalidShapes)
                {
                    Console.WriteLine(info);
                }
            }

            // Save the diagram (unchanged) if further processing is required
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
