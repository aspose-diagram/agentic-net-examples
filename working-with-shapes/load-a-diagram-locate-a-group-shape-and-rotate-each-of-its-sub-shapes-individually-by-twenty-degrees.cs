using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the group shape is on the first page
            Page page = diagram.Pages[0];

            // Locate the first group shape on the page
            foreach (Shape groupShape in page.Shapes)
            {
                if (groupShape.Type == TypeValue.Group)
                {
                    // Rotate each sub‑shape inside the group by 20 degrees
                    foreach (Shape subShape in groupShape.Shapes)
                    {
                        // Current rotation angle (in radians)
                        double currentAngle = subShape.XForm.Angle.Value;

                        // 20 degrees expressed in radians
                        double addedRadians = 20.0 * Math.PI / 180.0;

                        // Apply the new rotation
                        subShape.XForm.Angle.Value = currentAngle + addedRadians;
                    }

                    // If only one group shape is needed, break after processing
                    break;
                }
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
