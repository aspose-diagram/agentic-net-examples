using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, rotation angle in degrees
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <angleDegrees>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string angleArg = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double angleDegrees))
        {
            Console.Error.WriteLine($"Invalid rotation angle: {angleArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Pre‑compute rotation values
            double rad = angleDegrees * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve shape centre (PinX, PinY)
                    double centerX = shape.XForm.PinX.Value;
                    double centerY = shape.XForm.PinY.Value;

                    // Rotate each geometry vertex
                    for (int g = 0; g < shape.Geoms.Count; g++)
                    {
                        // Cast the geometry object
                        Geom geom = (Geom)shape.Geoms[g];

                        // Iterate over all coordinate objects in the geometry
                        for (int i = 0; i < geom.CoordinateCol.Count; i++)
                        {
                            object segment = geom.CoordinateCol[i];

                            // Handle MoveTo
                            if (segment is MoveTo move)
                            {
                                double x = move.X.Value;
                                double y = move.Y.Value;
                                double dx = x - centerX;
                                double dy = y - centerY;
                                double nx = dx * cos - dy * sin + centerX;
                                double ny = dx * sin + dy * cos + centerY;
                                move.X.Value = nx;
                                move.Y.Value = ny;
                            }
                            // Handle LineTo
                            else if (segment is LineTo line)
                            {
                                double x = line.X.Value;
                                double y = line.Y.Value;
                                double dx = x - centerX;
                                double dy = y - centerY;
                                double nx = dx * cos - dy * sin + centerX;
                                double ny = dx * sin + dy * cos + centerY;
                                line.X.Value = nx;
                                line.Y.Value = ny;
                            }
                            // Handle ArcTo (if present)
                            else if (segment is ArcTo arc)
                            {
                                double x = arc.X.Value;
                                double y = arc.Y.Value;
                                double dx = x - centerX;
                                double dy = y - centerY;
                                double nx = dx * cos - dy * sin + centerX;
                                double ny = dx * sin + dy * cos + centerY;
                                arc.X.Value = nx;
                                arc.Y.Value = ny;
                            }
                            // Additional geometry types (e.g., EllipticalArcTo) can be added here following the same pattern
                        }
                    }

                    // Apply the same visual rotation to the shape itself
                    shape.SetAngle(angleDegrees);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}