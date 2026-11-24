using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <expectedGeometryCount>");
            return;
        }

        string diagramPath = args[0];
        string expectedCountStr = args[1];

        // Guard: check file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse expected geometry count
        if (!int.TryParse(expectedCountStr, out int expectedCount) || expectedCount < 0)
        {
            Console.Error.WriteLine("Error: Expected geometry count must be a non‑negative integer.");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: Diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: First page contains no shapes.");
                return;
            }

            // Retrieve the first shape (skip logically deleted shapes)
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    shape = s;
                    break;
                }
            }

            if (shape == null)
            {
                Console.Error.WriteLine("Error: No non‑deleted shapes found on the first page.");
                return;
            }

            // Record original geometry count
            int originalGeomCount = shape.Geoms.Count;

            // Add a new geometry to the shape
            Geom newGeom = new Geom();
            shape.Geoms.Add(newGeom);

            // Validate the geometry count after modification
            int actualGeomCount = shape.Geoms.Count;

            if (actualGeomCount != expectedCount)
            {
                throw new Exception($"Geometry count mismatch. Expected: {expectedCount}, Actual: {actualGeomCount}");
            }
            else
            {
                Console.WriteLine($"Success: Geometry count is as expected ({actualGeomCount}).");
            }

            // Optional: Save the modified diagram (not required for validation)
            // string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "Modified_" + Path.GetFileName(diagramPath));
            // diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}