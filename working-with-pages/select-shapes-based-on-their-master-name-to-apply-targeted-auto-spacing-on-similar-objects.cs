using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the master name to target
            string targetMasterName = "Rectangle";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect IDs of shapes that use the specified master
                List<long> targetShapeIds = new List<long>();

                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master before accessing its name
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        targetShapeIds.Add(shape.ID);
                    }
                }

                // If any matching shapes are found, apply auto‑spacing
                if (targetShapeIds.Count > 0)
                {
                    // Configure auto‑spacing options (distance in inches)
                    AutoSpaceOptions options = new AutoSpaceOptions
                    {
                        DistanceInHorizontal = 0.5, // horizontal spacing
                        DistanceInVertical = 0.5    // vertical spacing
                    };

                    // Apply auto‑spacing to the entire page.
                    // Aspose.Diagram currently auto‑spaces the whole ShapeCollection;
                    // selecting specific shapes is demonstrated by the ID list above.
                    page.AutoSpaceShapes(page.Shapes, options);
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
