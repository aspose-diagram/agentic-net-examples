using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each Geom element of the shape
                    for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                    {
                        Geom geom = shape.Geoms[geomIndex];
                        Console.WriteLine($"Shape ID: {shape.ID}, Geom Index: {geomIndex}");

                        // Log MoveTo vertices
                        foreach (MoveTo move in geom.CoordinateCol.MoveToCol)
                        {
                            Console.WriteLine($"  MoveTo - X: {move.X.Value}, Y: {move.Y.Value}");
                        }

                        // Log LineTo vertices
                        foreach (LineTo line in geom.CoordinateCol.LineToCol)
                        {
                            Console.WriteLine($"  LineTo - X: {line.X.Value}, Y: {line.Y.Value}");
                        }

                        // Log ArcTo vertices (includes bow value A)
                        foreach (ArcTo arc in geom.CoordinateCol.ArcToCol)
                        {
                            Console.WriteLine($"  ArcTo - X: {arc.X.Value}, Y: {arc.Y.Value}, A (bow): {arc.A.Value}");
                        }

                        // Additional coordinate types can be logged similarly:
                        // foreach (Ellipse ellipse in geom.CoordinateCol.EllipseCol) { ... }
                        // foreach (PolylineTo poly in geom.CoordinateCol.PolylineToCol) { ... }
                        // etc.
                    }
                }
            }

            // Optional: save the diagram after processing
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
