using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Width threshold (in inches) – only shapes wider than this will be modified
            double widthThreshold = 2.0;

            // Load the diagram
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

                    // Check if the shape's width exceeds the threshold
                    if (shape.XForm.Width.Value > widthThreshold)
                    {
                        // Ensure the shape has at least one geometry section
                        if (shape.Geoms.Count == 0)
                            continue;

                        // Retrieve the first geometry section
                        Geom geom = (Geom)shape.Geoms[0];

                        // Create a new LineTo vertex (adds a line segment)
                        LineTo newVertex = new LineTo();
                        // Example: place the new vertex 0.5 inches to the right of the shape's current PinX
                        newVertex.X.Value = shape.XForm.PinX.Value + 0.5;
                        // Keep the same Y coordinate as the shape's PinY
                        newVertex.Y.Value = shape.XForm.PinY.Value;

                        // Append the new vertex to the geometry's coordinate collection
                        geom.CoordinateCol.Add(newVertex);
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
