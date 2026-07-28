using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_rotated.vsdx";

                // Index of the page whose group shapes should be rotated (0‑based)
                int pageIndex = 0;

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Validate page index
                    if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                    {
                        throw new Exception($"Page index {pageIndex} is out of range.");
                    }

                    // Retrieve the target page
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only group shapes
                        if (shape.Type == TypeValue.Group)
                        {
                            // Get the current rotation angle (degrees)
                            double currentAngle = shape.XForm.Angle.Value;

                            // Apply an additional 30° rotation
                            shape.XForm.Angle.Value = currentAngle + 30.0;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Rotation completed and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }