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

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Iterate through each Geom (geometry) of the shape
                    foreach (Geom geom in shape.Geoms)
                    {
                        // Log MoveTo vertices
                        foreach (MoveTo move in geom.CoordinateCol.MoveToCol)
                        {
                            Console.WriteLine($"Shape: {shape.Name}, Geom Index: {geom.IX}, MoveTo - X: {move.X.Value}, Y: {move.Y.Value}");
                        }

                        // Log LineTo vertices
                        foreach (LineTo line in geom.CoordinateCol.LineToCol)
                        {
                            Console.WriteLine($"Shape: {shape.Name}, Geom Index: {geom.IX}, LineTo - X: {line.X.Value}, Y: {line.Y.Value}");
                        }

                        // Log ArcTo vertices (includes bow A)
                        foreach (ArcTo arc in geom.CoordinateCol.ArcToCol)
                        {
                            Console.WriteLine($"Shape: {shape.Name}, Geom Index: {geom.IX}, ArcTo - X: {arc.X.Value}, Y: {arc.Y.Value}, A (bow): {arc.A.Value}");
                        }

                        // Log PolylineTo vertices
                        foreach (PolylineTo poly in geom.CoordinateCol.PolylineToCol)
                        {
                            Console.WriteLine($"Shape: {shape.Name}, Geom Index: {geom.IX}, PolylineTo - X: {poly.X.Value}, Y: {poly.Y.Value}, A (formula): {poly.A.Value}");
                        }

                        // Add other coordinate types as needed (e.g., RelLineTo, RelMoveTo, etc.)
                    }
                }
            }

            // Optionally save the diagram after processing
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
