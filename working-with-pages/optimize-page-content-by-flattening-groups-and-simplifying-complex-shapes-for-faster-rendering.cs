using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the optimized output file
                string outputPath = "optimized.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Flatten groups by hiding the group container shape
                        // If the shape is a group container, set its size to zero
                        if (shape.IsInGroup())
                        {
                            shape.XForm.Width.Value = 0;
                            shape.XForm.Height.Value = 0;
                        }

                        // Simplify complex shapes:
                        // Reduce line weight to a minimal value for faster rendering
                        shape.Line.LineWeight.Value = 0.01;

                        // Set a simple solid fill pattern (pattern index 1)
                        shape.Fill.FillPattern.Value = 1;

                        // Optional: set a basic line pattern (solid)
                        // Note: LinePatternValue.Solid is the typical enum member for a solid line
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                    }
                }

                // Save the optimized diagram using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }