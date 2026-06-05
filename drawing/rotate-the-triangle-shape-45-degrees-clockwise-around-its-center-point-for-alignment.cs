using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the triangle shape (by universal name) and rotate it 45° clockwise
            bool rotated = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        // Clockwise rotation: negative angle in radians
                        double angleRad = -Math.PI / 4.0; // -45 degrees
                        shape.SetAngle(angleRad);
                        rotated = true;
                        break;
                    }
                }
                if (rotated) break;
            }

            if (!rotated)
            {
                throw new Exception("Triangle shape not found in the diagram.");
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
