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

            // Load the Visio diagram from a file
            const string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    long shapeId = shape.ID; // Shape IDs are of type long

                    // Filter shapes with IDs greater than 100
                    if (shapeId > 100)
                    {
                        // Get current rotation angle (in degrees) and add 5 degrees
                        double currentAngle = shape.XForm.Angle.Value;
                        shape.XForm.Angle.Value = currentAngle + 5;
                    }
                }

                // Save the modified diagram
                const string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
