using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Select the specific page (e.g., first page)
                Page page = diagram.Pages[0];

                // Rotation to apply: 30 degrees expressed in radians
                double rotationRadians = Math.PI / 6.0;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify group shapes
                    if (shape.Type == TypeValue.Group)
                    {
                        // Add 30 degrees to the existing rotation angle
                        double currentAngle = shape.XForm.Angle.Value;
                        shape.XForm.Angle.Value = currentAngle + rotationRadians;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Rotation applied and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }