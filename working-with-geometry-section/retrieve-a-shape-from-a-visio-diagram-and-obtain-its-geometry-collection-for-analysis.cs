using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string filePath = "input.vsdx";
        // Guard to ensure the file exists before proceeding
        if (!File.Exists(filePath)) { Console.Error.WriteLine($"File not found: {filePath}"); return; }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Access the first page (or use diagram.Pages.GetPage("Page-1") for a named page)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (replace with a valid ID from your diagram)
            int shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Guard to ensure the shape was found
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Output basic shape information
            Console.WriteLine("Shape ID: " + shape.ID);
            Console.WriteLine("Shape NameU: " + shape.NameU);
            Console.WriteLine("Number of geometry sections: " + shape.Geoms.Count);

            // Iterate through each geometry (Geom) section
            foreach (Aspose.Diagram.Geom geom in shape.Geoms)
            {
                // Geom does not expose an Index property; output its hash code as an identifier
                Console.WriteLine("Geom Identifier: " + geom.GetHashCode());
                Console.WriteLine("Number of coordinate entries: " + geom.CoordinateCol.Count);

                // Iterate through each coordinate entry within the geometry
                foreach (object coord in geom.CoordinateCol)
                {
                    if (coord is MoveTo move)
                    {
                        // Output MoveTo coordinates
                        Console.WriteLine($"  MoveTo - X: {move.X.Value}, Y: {move.Y.Value}");
                    }
                    else if (coord is LineTo line)
                    {
                        // Output LineTo coordinates
                        Console.WriteLine($"  LineTo - X: {line.X.Value}, Y: {line.Y.Value}");
                    }
                    else if (coord is ArcTo arc)
                    {
                        // Output ArcTo coordinates
                        Console.WriteLine($"  ArcTo - X: {arc.X.Value}, Y: {arc.Y.Value}");
                    }
                    else if (coord is SplineKnot spline)
                    {
                        // Output SplineKnot coordinates
                        Console.WriteLine($"  SplineKnot - X: {spline.X.Value}, Y: {spline.Y.Value}");
                    }
                    // Additional coordinate types can be handled here if needed
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine("Error processing diagram: " + ex.Message);
        }
    }
}