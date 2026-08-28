using System.IO;
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
            const string inputPath = "input.vsdx";
            // Path for the optimized output
            const string outputPath = "optimized.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Collect shape IDs to avoid modification during enumeration
                    var shapeIds = new System.Collections.Generic.List<long>();
                    foreach (Shape shape in page.Shapes)
                    {
                        shapeIds.Add(shape.ID);
                    }

                    // Process each shape
                    foreach (long shapeId in shapeIds)
                    {
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape is a group, ungroup it to flatten the hierarchy
                        if (shape.Type == TypeValue.Group)
                        {
                            shape.Ungroup();
                            // After ungrouping, the original group shape is removed automatically
                            continue;
                        }

                        // Simplify complex shapes by reducing line and fill details
                        // Set a simple solid line pattern
                        shape.Line.LinePattern.Value = LinePatternValue.Solid;
                        // Reduce line weight to a minimal value
                        shape.Line.LineWeight.Value = 0.02;
                        // Remove fill pattern (set to none)
                        shape.Fill.FillPattern.Value = 0;
                    }
                }

                // Save the optimized diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram optimization completed successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
