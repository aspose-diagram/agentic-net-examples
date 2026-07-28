using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Ensure the page has shapes
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Find the first shape that has a gradient fill enabled
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape with a gradient fill was found on the first page.");
                    return;
                }

                // Read the gradient direction value
                double gradientDirection = targetShape.Fill.GradientFill.GradientDir.Value;

                // Output the result for verification
                Console.WriteLine($"Gradient direction (numeric value) of the selected shape: {gradientDirection}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }