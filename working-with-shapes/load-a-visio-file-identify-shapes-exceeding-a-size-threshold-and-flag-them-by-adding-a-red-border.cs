using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_flagged.vsdx";

                // Size threshold in inches (shapes larger than this will be flagged)
                const double sizeThresholdInches = 2.0;

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve shape dimensions
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Check if the shape exceeds the size threshold
                        if (width > sizeThresholdInches || height > sizeThresholdInches)
                        {
                            // Flag the shape by adding a red border
                            shape.Line.LineColor.Value = "#FF0000";   // Red color
                            shape.Line.LineWeight.Value = 0.03;       // Thickness in inches
                            // Optional: set a solid line pattern
                            // shape.Line.LinePattern.Value = LinePatternValue.Solid;
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