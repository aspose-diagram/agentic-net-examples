using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes).
                        if (shape.OneD)
                        {
                            // Set the connector routing style to straight for clearer routing.
                            // ShapeRouteStyleValue enum members include Straight, RightAngle, etc.
                            shape.Layout.ShapeRouteStyle.Value = ShapeRouteStyleValue.Straight;

                            // Directly edit the geometry section of the connector.
                            // The geometry collection (Geoms) holds the path segments.
                            if (shape.Geoms != null && shape.Geoms.Count > 0)
                            {
                                // Access the first geometry (usually the only one for a connector).
                                Geom geom = (Geom)shape.Geoms[0];

                                // Ensure there is at least one coordinate segment to modify.
                                if (geom.CoordinateCol != null && geom.CoordinateCol.Count > 0)
                                {
                                    // Retrieve the first segment (could be MoveTo or LineTo).
                                    object firstSeg = geom.CoordinateCol[0];

                                    // Adjust the X coordinate slightly to avoid overlap.
                                    if (firstSeg is MoveTo move)
                                    {
                                        move.X.Value += 0.05;
                                    }
                                    else if (firstSeg is LineTo line)
                                    {
                                        line.X.Value += 0.05;
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}