using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Retrieves a list of vertex coordinates (X,Y) from the geometry of a shape.
    static List<(double X, double Y)> GetVertexList(Shape shape)
    {
        var vertices = new List<(double X, double Y)>();

        // Iterate over each geometry in the shape.
        foreach (Geom geom in shape.Geoms)
        {
            // Iterate over each segment in the geometry's coordinate collection.
            foreach (object segment in geom.CoordinateCol)
            {
                // MoveTo and LineTo both contain X and Y values.
                if (segment is MoveTo moveTo)
                {
                    vertices.Add((moveTo.X.Value, moveTo.Y.Value));
                }
                else if (segment is LineTo lineTo)
                {
                    vertices.Add((lineTo.X.Value, lineTo.Y.Value));
                }
                // Other segment types (e.g., ArcTo) are ignored for this comparison.
            }
        }

        return vertices;
    }

    // Compares two vertex lists and returns true if they are identical in order and values.
    static bool CompareVertexLists(List<(double X, double Y)> before, List<(double X, double Y)> after)
    {
        if (before.Count != after.Count)
            return false;

        for (int i = 0; i < before.Count; i++)
        {
            if (Math.Abs(before[i].X - after[i].X) > 1e-6 ||
                Math.Abs(before[i].Y - after[i].Y) > 1e-6)
                return false;
        }

        return true;
    }

    static void Main(string[] args)
    {
        // Expect input diagram path as first argument and optional output path as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path> [output-diagram-path]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "modified_output.vsdx";

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape to work with.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page firstPage = diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Select the first shape for geometry comparison.
            Shape shape = firstPage.Shapes[0];

            // Capture vertex list before modification.
            List<(double X, double Y)> beforeVertices = GetVertexList(shape);
            Console.WriteLine($"Vertex count before modification: {beforeVertices.Count}");

            // Modify geometry: add a new LineTo vertex to the first geometry.
            if (shape.Geoms.Count == 0)
            {
                Console.Error.WriteLine("Selected shape has no geometry sections.");
                return;
            }

            // Access the first geometry.
            Geom targetGeom = (Geom)shape.Geoms[0];

            // Create a new LineTo segment.
            LineTo newSegment = new LineTo();
            newSegment.X.Value = 2.0; // Example X coordinate.
            newSegment.Y.Value = 2.0; // Example Y coordinate.

            // Append the new segment to the geometry's coordinate collection.
            targetGeom.CoordinateCol.Add(newSegment);

            // Capture vertex list after modification.
            List<(double X, double Y)> afterVertices = GetVertexList(shape);
            Console.WriteLine($"Vertex count after modification: {afterVertices.Count}");

            // Verify that the new vertex was added.
            bool isAdded = afterVertices.Count == beforeVertices.Count + 1 &&
                           Math.Abs(afterVertices[afterVertices.Count - 1].X - newSegment.X.Value) < 1e-6 &&
                           Math.Abs(afterVertices[afterVertices.Count - 1].Y - newSegment.Y.Value) < 1e-6;

            if (isAdded)
            {
                Console.WriteLine("Vertex addition verified successfully.");
            }
            else
            {
                Console.Error.WriteLine("Vertex addition verification failed.");
                throw new Exception("Geometry modification did not produce expected vertex list.");
            }

            // Optional: compare full lists to ensure only the intended change occurred.
            // Remove the last vertex from after list for comparison.
            List<(double X, double Y)> afterWithoutNew = new List<(double X, double Y)>(afterVertices);
            afterWithoutNew.RemoveAt(afterWithoutNew.Count - 1);

            if (CompareVertexLists(beforeVertices, afterWithoutNew))
            {
                Console.WriteLine("All existing vertices remain unchanged.");
            }
            else
            {
                Console.Error.WriteLine("Existing vertices were altered unexpectedly.");
                throw new Exception("Unexpected changes detected in geometry vertices.");
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}