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
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (or iterate all pages if needed)
                Page page = diagram.Pages[0];

                // Apply Perspective rotation type and set a 45‑degree perspective angle
                foreach (Shape shape in page.Shapes)
                {
                    // Set rotation type to Perspective
                    shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

                    // Assign a 45‑degree perspective angle
                    shape.ThreeDFormat.Perspective.Value = 45;
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