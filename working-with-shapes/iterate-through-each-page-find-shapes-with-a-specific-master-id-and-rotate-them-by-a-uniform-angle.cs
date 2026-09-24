using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to save the modified Visio file
            string outputPath = "output.vsdx";

            // Master ID to filter shapes (adjust as needed)
            int targetMasterId = 5;
            // Uniform rotation angle in degrees
            double rotationAngle = 45.0;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that have a master with the specified ID
                    if (shape.Master != null && shape.Master.ID == targetMasterId)
                    {
                        // Apply the rotation
                        shape.XForm.Angle.Value = rotationAngle;
                    }
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
