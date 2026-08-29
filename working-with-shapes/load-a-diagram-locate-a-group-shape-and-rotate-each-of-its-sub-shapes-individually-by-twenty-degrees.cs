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
                string outputPath = "rotated.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page to find group shapes
                foreach (Shape groupShape in page.Shapes)
                {
                    // Identify a group shape by its Type
                    if (groupShape.Type == TypeValue.Group)
                    {
                        // Iterate through each sub‑shape within the group
                        foreach (Shape subShape in groupShape.Shapes)
                        {
                            // Current rotation angle is stored in radians
                            double currentAngle = subShape.XForm.Angle.Value;

                            // Convert 20 degrees to radians
                            double offsetRadians = 20.0 * Math.PI / 180.0;

                            // Apply the rotation increment
                            subShape.XForm.Angle.Value = currentAngle + offsetRadians;
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