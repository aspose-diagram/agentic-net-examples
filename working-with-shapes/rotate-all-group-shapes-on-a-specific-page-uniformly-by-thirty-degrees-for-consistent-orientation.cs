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
                // Path for the rotated output file
                string outputPath = "output_rotated.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Specify the page index (0‑based). Change as needed.
                int pageIndex = 0;
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.WriteLine("Invalid page index.");
                    return;
                }

                // Retrieve the target page
                Page page = diagram.Pages[pageIndex];

                // Rotation increment: 30 degrees expressed in radians
                double rotationIncrement = Math.PI / 6.0; // 30° = π/6 rad

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only group shapes
                    if (shape.Type == TypeValue.Group)
                    {
                        // Add 30° to the existing rotation (angle is stored in radians)
                        shape.XForm.Angle.Value += rotationIncrement;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Rotation applied and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }