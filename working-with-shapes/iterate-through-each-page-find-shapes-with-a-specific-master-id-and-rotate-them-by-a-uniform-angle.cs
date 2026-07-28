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
                // Path where the modified file will be saved
                string outputPath = "output.vsdx";

                // The master ID to filter shapes
                int targetMasterId = 5; // <-- set the desired master ID
                // Rotation angle in degrees (uniform for all matched shapes)
                double rotationAngle = 45.0; // <-- set the desired angle

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check that the shape has a master and that its master ID matches the target
                            if (shape.Master != null && shape.Master.ID == targetMasterId)
                            {
                                // Apply the rotation (Angle cell expects degrees)
                                shape.XForm.Angle.Value = rotationAngle;
                            }
                        }
                    }

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine($"Rotated all shapes with master ID {targetMasterId} by {rotationAngle} degrees.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }