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
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes with an ID greater than 100
                    if (shape.ID > 100L)
                    {
                        // Current rotation angle is stored in radians
                        double currentAngle = shape.XForm.Angle.Value;

                        // Convert 5 degrees to radians
                        double delta = 5.0 * Math.PI / 180.0;

                        // Apply the additional rotation
                        shape.XForm.Angle.Value = currentAngle + delta;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
