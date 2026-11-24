using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path, rotation angle (degrees), optional output path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <rotationAngleDegrees> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string angleArg = args[1];
        string outputPath = args.Length >= 3
            ? args[2]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? string.Empty, "rotated.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double angleDeg))
        {
            Console.Error.WriteLine($"Invalid rotation angle: {angleArg}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Convert angle to radians for trigonometric calculations
                double angleRad = angleDeg * Math.PI / 180.0;
                double cos = Math.Cos(angleRad);
                double sin = Math.Sin(angleRad);

                // Iterate through all pages
                for (int p = 0; p < diagram.Pages.Count; p++)
                {
                    Page page = diagram.Pages[p];

                    // Iterate through all shapes on the page
                    for (int s = 0; s < page.Shapes.Count; s++)
                    {
                        Shape shape = page.Shapes[s];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine the rotation centre (shape's PinX/PinY)
                        double centerX = shape.XForm.PinX.Value;
                        double centerY = shape.XForm.PinY.Value;

                        // Iterate through each geometry (Geom) of the shape
                        for (int g = 0; g < shape.Geoms.Count; g++)
                        {
                            Geom geom = shape.Geoms[g];

                            // Iterate through each coordinate object in the geometry
                            for (int c = 0; c < geom.CoordinateCol.Count; c++)
                            {
                                // Retrieve the current coordinate object
                                object coordObj = geom.CoordinateCol[c];

                                // Handle MoveTo geometry segment
                                if (coordObj is MoveTo moveTo)
                                {
                                    double x = moveTo.X.Value;
                                    double y = moveTo.Y.Value;

                                    // Translate point to origin relative to centre
                                    double dx = x - centerX;
                                    double dy = y - centerY;

                                    // Apply rotation
                                    double rotatedX = dx * cos - dy * sin;
                                    double rotatedY = dx * sin + dy * cos;

                                    // Translate back to original centre
                                    moveTo.X.Value = rotatedX + centerX;
                                    moveTo.Y.Value = rotatedY + centerY;
                                }
                                // Handle LineTo geometry segment
                                else if (coordObj is LineTo lineTo)
                                {
                                    double x = lineTo.X.Value;
                                    double y = lineTo.Y.Value;

                                    double dx = x - centerX;
                                    double dy = y - centerY;

                                    double rotatedX = dx * cos - dy * sin;
                                    double rotatedY = dx * sin + dy * cos;

                                    lineTo.X.Value = rotatedX + centerX;
                                    lineTo.Y.Value = rotatedY + centerY;
                                }
                                // Handle other possible coordinate types (e.g., ArcTo) that also expose X/Y
                                else if (coordObj is ArcTo arcTo)
                                {
                                    double x = arcTo.X.Value;
                                    double y = arcTo.Y.Value;

                                    double dx = x - centerX;
                                    double dy = y - centerY;

                                    double rotatedX = dx * cos - dy * sin;
                                    double rotatedY = dx * sin + dy * cos;

                                    arcTo.X.Value = rotatedX + centerX;
                                    arcTo.Y.Value = rotatedY + centerY;
                                }
                                // Add handling for additional coordinate types if needed
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path using the correct overload
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}