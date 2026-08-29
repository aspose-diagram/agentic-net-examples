using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Master ID to filter shapes (replace with the actual ID you need)
            int targetMasterId = 5;

            // Desired rotation angle in degrees
            double rotationAngle = 45.0;

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check that the shape has a master and that its master ID matches the target
                        if (shape.Master != null && shape.Master.ID == targetMasterId)
                        {
                            // Rotate the shape by setting the XForm.Angle property (degrees)
                            shape.XForm.Angle.Value = rotationAngle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Shapes with master ID {0} rotated by {1} degrees and saved to {2}.",
                targetMasterId, rotationAngle, outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
