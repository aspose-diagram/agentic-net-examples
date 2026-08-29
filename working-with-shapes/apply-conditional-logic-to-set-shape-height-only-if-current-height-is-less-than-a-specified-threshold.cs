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

                // Height threshold (in inches). Shapes with a height less than this will be updated.
                double heightThreshold = 2.0;

                // Desired height to set when the condition is met (also in inches)
                double targetHeight = 2.0;

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the current height of the shape
                        double currentHeight = shape.XForm.Height.Value;

                        // Apply conditional logic: set new height only if current height is below the threshold
                        if (currentHeight < heightThreshold)
                        {
                            shape.XForm.Height.Value = targetHeight;
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