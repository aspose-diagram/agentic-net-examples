using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, shape NameU, output JSON path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <shapeNameU> <outputJsonPath>");
            return;
        }

        string diagramPath = args[0];
        string shapeNameU = args[1];
        string outputJsonPath = args[2];

        // Guard: ensure input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Locate the shape by its NameU across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // NameU is a plain string, not a cell object
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU == shapeNameU)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with NameU '{shapeNameU}' not found in the diagram.");
                    return;
                }

                // Prepare a list to hold geometry sections data
                var geometrySections = new List<GeometrySectionDto>();

                // Iterate over each geometry (section) of the shape
                for (int secIdx = 0; secIdx < targetShape.Geoms.Count; secIdx++)
                {
                    // Explicitly type the geometry object
                    Geom geom = (Geom)targetShape.Geoms[secIdx];
                    var points = new List<PointDto>();

                    // Iterate over each coordinate row within the geometry
                    for (int rowIdx = 0; rowIdx < geom.CoordinateCol.Count; rowIdx++)
                    {
                        // The collection returns objects of various geometry row types
                        object row = geom.CoordinateCol[rowIdx];

                        // Handle MoveTo rows
                        if (row is MoveTo move)
                        {
                            points.Add(new PointDto { X = move.X.Value, Y = move.Y.Value });
                        }
                        // Handle LineTo rows
                        else if (row is LineTo line)
                        {
                            points.Add(new PointDto { X = line.X.Value, Y = line.Y.Value });
                        }
                        // Handle ArcTo rows
                        else if (row is ArcTo arc)
                        {
                            points.Add(new PointDto { X = arc.X.Value, Y = arc.Y.Value });
                        }
                        // Handle EllipticalArcTo rows
                        else if (row is EllipticalArcTo ell)
                        {
                            points.Add(new PointDto { X = ell.X.Value, Y = ell.Y.Value });
                        }
                        // Handle SplineKnot rows
                        else if (row is SplineKnot knot)
                        {
                            points.Add(new PointDto { X = knot.X.Value, Y = knot.Y.Value });
                        }
                        // Other row types can be added here as needed
                    }

                    geometrySections.Add(new GeometrySectionDto
                    {
                        SectionIndex = secIdx,
                        Points = points
                    });
                }

                // Serialize the collected geometry data to JSON
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(geometrySections, jsonOptions);

                // Write JSON to the specified output file
                File.WriteAllText(outputJsonPath, json);
                Console.WriteLine($"Geometry data for shape '{shapeNameU}' has been serialized to '{outputJsonPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // DTO for a geometry section
    private class GeometrySectionDto
    {
        public int SectionIndex { get; set; }
        public List<PointDto> Points { get; set; } = new();
    }

    // DTO for a point (X,Y)
    private class PointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
