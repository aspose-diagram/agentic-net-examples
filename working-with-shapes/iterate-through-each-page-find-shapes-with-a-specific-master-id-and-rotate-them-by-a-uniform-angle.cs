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

                // Target master ID and rotation angle (degrees)
                int targetMasterId = 5; // replace with actual master ID
                double rotationDegrees = 45.0;

                // Convert degrees to radians for the Angle cell
                double rotationRadians = Math.PI * rotationDegrees / 180.0;

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Check if the shape uses the specified master
                            if (shape.Master != null && shape.Master.ID == targetMasterId)
                            {
                                // Apply rotation (radians)
                                shape.XForm.Angle.Value = rotationRadians;
                            }
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