using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, shape ID, scale factor
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> <shapeId> <scaleFactor>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string shapeIdArg = args[2];
        string scaleFactorArg = args[3];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape ID
        if (!long.TryParse(shapeIdArg, out long targetShapeId))
        {
            Console.Error.WriteLine($"Invalid shape ID: {shapeIdArg}");
            return;
        }

        // Parse scale factor
        if (!double.TryParse(scaleFactorArg, out double scaleFactor) || scaleFactor <= 0)
        {
            Console.Error.WriteLine($"Invalid scale factor: {scaleFactorArg}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape targetShape = null;

                // Locate the shape by ID across all pages
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ID == targetShapeId)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                    return;
                }

                // Ensure the shape has at least one geometry
                if (targetShape.Geoms == null || targetShape.Geoms.Count == 0)
                {
                    Console.Error.WriteLine("Target shape does not contain any geometry.");
                    return;
                }

                // Iterate over each geometry (most shapes have one)
                for (int g = 0; g < targetShape.Geoms.Count; g++)
                {
                    var geometry = targetShape.Geoms[g];

                    // Iterate over each vertex segment in the geometry
                    for (int i = 0; i < geometry.CoordinateCol.Count; i++)
                    {
                        var segment = geometry.CoordinateCol[i];

                        // Update MoveTo vertices
                        if (segment is MoveTo moveTo)
                        {
                            moveTo.X.Value *= scaleFactor;
                            moveTo.Y.Value *= scaleFactor;
                        }
                        // Update LineTo vertices
                        else if (segment is LineTo lineTo)
                        {
                            lineTo.X.Value *= scaleFactor;
                            lineTo.Y.Value *= scaleFactor;
                        }
                        // Update ArcTo vertices (if present)
                        else if (segment is ArcTo arcTo)
                        {
                            arcTo.X.Value *= scaleFactor;
                            arcTo.Y.Value *= scaleFactor;
                        }
                        // Other segment types can be handled similarly if needed
                    }
                }

                // Save the modified diagram to the output path using VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}