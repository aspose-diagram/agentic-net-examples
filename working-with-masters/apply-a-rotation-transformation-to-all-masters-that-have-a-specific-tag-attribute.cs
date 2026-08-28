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

            // The tag value that identifies shapes to rotate
            string targetTag = "Rotate";

            // Rotation angle in radians (45 degrees)
            double rotationRadians = Math.PI / 4;

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Iterate through each shape contained in the master
                foreach (Shape shape in master.Shapes)
                {
                    // Ensure the shape has custom properties (Props)
                    if (shape.Props != null)
                    {
                        // Look for a custom property named "Tag" with the desired value
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "Tag" && prop.Value != null && prop.Value.Val == targetTag)
                            {
                                // Apply the rotation to the shape
                                shape.XForm.Angle.Value = rotationRadians;
                                break; // Tag found, no need to check other props for this shape
                            }
                        }
                    }
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
