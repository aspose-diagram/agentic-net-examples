using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (use defaults if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page and one shape
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page firstPage = (Page)diagram.Pages[0];
            if (firstPage.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Work with the first shape on the first page
            Shape shape = (Shape)firstPage.Shapes[0];

            // Capture geometry vertices before modification
            List<(double X, double Y)> beforeVertices = new List<(double, double)>();
            for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
            {
                Geom geom = (Geom)shape.Geoms[geomIndex];
                for (int segIndex = 0; segIndex < geom.CoordinateCol.Count; segIndex++)
                {
                    object segment = geom.CoordinateCol[segIndex];
                    if (segment is MoveTo)
                    {
                        MoveTo move = (MoveTo)segment;
                        beforeVertices.Add((move.X.Value, move.Y.Value));
                    }
                    else if (segment is LineTo)
                    {
                        LineTo line = (LineTo)segment;
                        beforeVertices.Add((line.X.Value, line.Y.Value));
                    }
                    else if (segment is ArcTo)
                    {
                        ArcTo arc = (ArcTo)segment;
                        beforeVertices.Add((arc.X.Value, arc.Y.Value));
                    }
                    // Additional segment types can be handled here if needed
                }
            }

            // Modify geometry: add a new vertex to the first geometry
            if (shape.Geoms.Count == 0)
            {
                Console.Error.WriteLine("The shape has no geometry sections to modify.");
                return;
            }

            Geom targetGeom = (Geom)shape.Geoms[0];
            LineTo newVertex = new LineTo();
            newVertex.X.Value = 5.0; // Example X coordinate
            newVertex.Y.Value = 5.0; // Example Y coordinate
            targetGeom.CoordinateCol.Add(newVertex);

            // Capture geometry vertices after modification
            List<(double X, double Y)> afterVertices = new List<(double, double)>();
            for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
            {
                Geom geom = (Geom)shape.Geoms[geomIndex];
                for (int segIndex = 0; segIndex < geom.CoordinateCol.Count; segIndex++)
                {
                    object segment = geom.CoordinateCol[segIndex];
                    if (segment is MoveTo)
                    {
                        MoveTo move = (MoveTo)segment;
                        afterVertices.Add((move.X.Value, move.Y.Value));
                    }
                    else if (segment is LineTo)
                    {
                        LineTo line = (LineTo)segment;
                        afterVertices.Add((line.X.Value, line.Y.Value));
                    }
                    else if (segment is ArcTo)
                    {
                        ArcTo arc = (ArcTo)segment;
                        afterVertices.Add((arc.X.Value, arc.Y.Value));
                    }
                }
            }

            // Verify that the modification was applied correctly
            if (afterVertices.Count != beforeVertices.Count + 1)
            {
                throw new Exception($"Vertex count mismatch. Expected {beforeVertices.Count + 1}, but got {afterVertices.Count}.");
            }

            // Check that the newly added vertex matches the expected coordinates
            (double X, double Y) addedVertex = afterVertices[afterVertices.Count - 1];
            if (Math.Abs(addedVertex.X - newVertex.X.Value) > 1e-6 || Math.Abs(addedVertex.Y - newVertex.Y.Value) > 1e-6)
            {
                throw new Exception("The added vertex does not match the expected coordinates.");
            }

            Console.WriteLine("Geometry modification verified successfully.");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}