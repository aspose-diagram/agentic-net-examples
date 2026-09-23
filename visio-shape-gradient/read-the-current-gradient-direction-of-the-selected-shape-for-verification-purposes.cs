using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify that the shape has gradient fill enabled
                    if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        // Read the current gradient direction value
                        double gradientDirection = shape.Fill.GradientFill.GradientDir.Value;

                        // Output the shape ID and its gradient direction
                        Console.WriteLine($"Shape ID {shape.ID} gradient direction: {gradientDirection}");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }