using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the diagram from file
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Define the uniform scaling factor (e.g., 2.0 for 200% enlargement)
        double scaleFactor = 2.0;

        try
        {
            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process each geometry (Geom) of the shape
                    foreach (Geom geom in shape.Geoms)
                    {
                        // Iterate over each vertex/segment in the geometry's coordinate collection
                        foreach (object segment in geom.CoordinateCol)
                        {
                            // MoveTo vertex
                            if (segment is MoveTo moveTo)
                            {
                                moveTo.X.Value *= scaleFactor;
                                moveTo.Y.Value *= scaleFactor;
                            }
                            // LineTo vertex
                            else if (segment is LineTo lineTo)
                            {
                                lineTo.X.Value *= scaleFactor;
                                lineTo.Y.Value *= scaleFactor;
                            }
                            // ArcTo vertex
                            else if (segment is ArcTo arcTo)
                            {
                                arcTo.X.Value *= scaleFactor;
                                arcTo.Y.Value *= scaleFactor;
                            }
                            // EllipticalArcTo vertex (correct class name)
                            else if (segment is EllipticalArcTo ellipticalArcTo)
                            {
                                ellipticalArcTo.X.Value *= scaleFactor;
                                ellipticalArcTo.Y.Value *= scaleFactor;
                            }
                            // SplineKnot vertex
                            else if (segment is SplineKnot splineKnot)
                            {
                                splineKnot.X.Value *= scaleFactor;
                                splineKnot.Y.Value *= scaleFactor;
                            }
                            // Additional vertex types can be added here following the same pattern
                        }
                    }

                    // Refresh shape data to ensure geometry changes are applied
                    shape.RefreshData();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during scaling operation: {ex.Message}");
            return;
        }

        // Save the modified diagram to a new file
        string outputPath = "scaled_output.vsdx";
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}