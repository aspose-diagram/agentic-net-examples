using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <sourceShapeNameU> <targetShapeNameU>");
            return;
        }

        string diagramPath = args[0];
        string sourceShapeNameU = args[1];
        string targetShapeNameU = args[2];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found – {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                Shape sourceShape = null;
                Shape targetShape = null;

                // Locate source and target shapes by their NameU property.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals(sourceShapeNameU, StringComparison.OrdinalIgnoreCase))
                        {
                            sourceShape = shape;
                        }

                        if (shape.NameU != null && shape.NameU.Equals(targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                        {
                            targetShape = shape;
                        }

                        // Break early if both shapes are found.
                        if (sourceShape != null && targetShape != null)
                            break;
                    }

                    if (sourceShape != null && targetShape != null)
                        break;
                }

                // Verify that both shapes were found.
                if (sourceShape == null)
                {
                    Console.Error.WriteLine($"Error: Source shape \"{sourceShapeNameU}\" not found.");
                    return;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Target shape \"{targetShapeNameU}\" not found.");
                    return;
                }

                // Remove any existing geometry from the target shape.
                if (targetShape.Geoms != null)
                {
                    while (targetShape.Geoms.Count > 0)
                    {
                        // RemoveAt removes the geometry at the specified index.
                        targetShape.Geoms.RemoveAt(0);
                    }
                }

                // Clone geometry from source shape to target shape.
                if (sourceShape.Geoms != null)
                {
                    for (int i = 0; i < sourceShape.Geoms.Count; i++)
                    {
                        // Retrieve the source geometry.
                        Geom srcGeom = (Geom)sourceShape.Geoms[i];

                        // Add the geometry to the target shape.
                        // Adding the same Geom instance effectively clones its data.
                        targetShape.Geoms.Add(srcGeom);
                    }
                }

                // Define output path.
                string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "ClonedGeometry.vsdx");

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Geometry cloned from \"{sourceShapeNameU}\" to \"{targetShapeNameU}\" and saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}