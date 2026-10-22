using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("Diagram contains no pages.");
                }

                Page page = diagram.Pages[0];

                // -------------------------------------------------------------
                // 1. Add a new rectangle shape to the page.
                // -------------------------------------------------------------
                // The fourth parameter (isCalculate) is a plain bool, not BOOL.
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify that the shape has at least one geometry section.
                if (shape.Geoms == null || shape.Geoms.Count == 0)
                {
                    throw new Exception("Added shape does not contain any geometry sections.");
                }

                // Retrieve the first geometry (explicit cast to Geom).
                Geom geometry = (Geom)shape.Geoms[0];

                // -------------------------------------------------------------
                // 2. Verify initial vertex count.
                // -------------------------------------------------------------
                int initialVertexCount = geometry.CoordinateCol.Count;
                Console.WriteLine($"Initial vertex count: {initialVertexCount}");

                // -------------------------------------------------------------
                // 3. Add a new vertex (LineTo) to the geometry.
                // -------------------------------------------------------------
                LineTo newVertex = new LineTo();
                newVertex.X.Value = 5.0;
                newVertex.Y.Value = 7.0;
                geometry.CoordinateCol.Add(newVertex);

                // Verify the vertex was added.
                int afterAddCount = geometry.CoordinateCol.Count;
                if (afterAddCount != initialVertexCount + 1)
                {
                    throw new Exception("Vertex addition failed: count mismatch.");
                }

                // Retrieve the last vertex and verify its coordinates.
                object addedObj = geometry.CoordinateCol[afterAddCount - 1];
                if (addedObj is LineTo addedLine)
                {
                    if (Math.Abs(addedLine.X.Value - 5.0) > 0.0001 ||
                        Math.Abs(addedLine.Y.Value - 7.0) > 0.0001)
                    {
                        throw new Exception("Added vertex coordinates do not match expected values.");
                    }
                }
                else
                {
                    throw new Exception("Added geometry element is not a LineTo.");
                }
                Console.WriteLine("Vertex addition verified.");

                // -------------------------------------------------------------
                // 4. Update the first vertex's coordinates.
                // -------------------------------------------------------------
                if (geometry.CoordinateCol.Count == 0)
                {
                    throw new Exception("No vertices available to update.");
                }

                object firstObj = geometry.CoordinateCol[0];
                if (firstObj is MoveTo firstMove)
                {
                    firstMove.X.Value = 1.5;
                    firstMove.Y.Value = 2.5;
                }
                else if (firstObj is LineTo firstLine)
                {
                    firstLine.X.Value = 1.5;
                    firstLine.Y.Value = 2.5;
                }
                else
                {
                    throw new Exception("First geometry element is neither MoveTo nor LineTo.");
                }

                // Verify the update.
                object verifyObj = geometry.CoordinateCol[0];
                double xVal, yVal;
                if (verifyObj is MoveTo mv)
                {
                    xVal = mv.X.Value;
                    yVal = mv.Y.Value;
                }
                else if (verifyObj is LineTo ln)
                {
                    xVal = ln.X.Value;
                    yVal = ln.Y.Value;
                }
                else
                {
                    throw new Exception("Unexpected geometry element type during verification.");
                }

                if (Math.Abs(xVal - 1.5) > 0.0001 || Math.Abs(yVal - 2.5) > 0.0001)
                {
                    throw new Exception("Vertex update failed: coordinates not set correctly.");
                }
                Console.WriteLine("Vertex update verified.");

                // -------------------------------------------------------------
                // 5. Remove the vertex we added earlier (last vertex).
                // -------------------------------------------------------------
                int indexToRemove = geometry.CoordinateCol.Count - 1;
                geometry.CoordinateCol.RemoveAt(indexToRemove);

                // Verify removal.
                int afterRemoveCount = geometry.CoordinateCol.Count;
                if (afterRemoveCount != afterAddCount - 1)
                {
                    throw new Exception("Vertex removal failed: count mismatch.");
                }
                Console.WriteLine("Vertex removal verified.");

                // -------------------------------------------------------------
                // 6. Save the modified diagram.
                // -------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}