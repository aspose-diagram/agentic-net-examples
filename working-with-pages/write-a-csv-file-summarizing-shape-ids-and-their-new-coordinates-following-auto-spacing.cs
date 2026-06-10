using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and output CSV file paths
            string diagramPath = "input.vsdx";
            string csvPath = "shape_coords.csv";

            // Load the diagram from file
            Diagram diagram = new Diagram(diagramPath);

            // Create CSV file and write header
            using (StreamWriter csvWriter = new StreamWriter(csvPath))
            {
                csvWriter.WriteLine("PageName,ShapeID,PinX,PinY");

                // Process each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Apply auto‑spacing to the shapes on the current page
                    AutoSpaceOptions options = new AutoSpaceOptions();
                    options.DistanceInHorizontal = 2; // horizontal spacing in inches
                    options.DistanceInVertical = 2;   // vertical spacing in inches
                    page.AutoSpaceShapes(page.Shapes, options);

                    // Record the new coordinates of each non‑deleted shape
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        long shapeId = shape.ID;
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        csvWriter.WriteLine($"{page.Name},{shapeId},{pinX},{pinY}");
                    }
                }
            }

            // Diagram can be saved later if needed
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
