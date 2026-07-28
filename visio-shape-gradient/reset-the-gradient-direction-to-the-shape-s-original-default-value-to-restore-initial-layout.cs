using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from the input file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape has a gradient fill enabled
                        if (shape.Fill.GradientFill.GradientEnabled.Value == BOOL.True)
                        {
                            // Reset the gradient direction to the default (0)
                            shape.Fill.GradientFill.GradientDir.Value = 0;
                        }
                    }
                }

                // Save the modified diagram, preserving the original format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }