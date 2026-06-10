using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the tag name and the value that identifies masters to rotate
                const string tagName = "Tag";
                const string tagValue = "RotateMe";

                // Desired rotation angle in degrees
                const double rotationAngle = 45.0;

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Iterate through each shape that belongs to the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Ensure the shape has custom properties (Props) before accessing them
                        if (shape.Props == null) continue;

                        // Look for a custom property with the specified tag name and value
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Label != null && prop.Label.Value == tagName &&
                                prop.Value != null && prop.Value.Val == tagValue)
                            {
                                // Apply the rotation to the shape within the master
                                shape.XForm.Angle.Value = rotationAngle;
                                // No need to continue checking other properties for this shape
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }