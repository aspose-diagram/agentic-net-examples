using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Retrieve the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes[0];

                // Output basic shape information.
                Console.WriteLine($"Shape ID: {shape.ID}");
                Console.WriteLine($"Shape NameU: {shape.NameU}");
                Console.WriteLine($"Shape Master Name: {(shape.Master != null ? shape.Master.Name : "None")}");

                // Access the Geometry (Geoms) collection.
                if (shape.Geoms == null || shape.Geoms.Count == 0)
                {
                    Console.WriteLine("The shape has no geometry sections.");
                }
                else
                {
                    Console.WriteLine($"Geometry sections count: {shape.Geoms.Count}");

                    // Iterate over each geometry section.
                    for (int i = 0; i < shape.Geoms.Count; i++)
                    {
                        // Explicitly type the geometry object.
                        Geom geom = shape.Geoms[i];
                        Console.WriteLine($"  Geometry {i + 1}:");

                        // Ensure the geometry has a coordinate collection.
                        if (geom.CoordinateCol == null || geom.CoordinateCol.Count == 0)
                        {
                            Console.WriteLine("    No coordinates in this geometry.");
                            continue;
                        }

                        // Iterate over each coordinate.
                        for (int j = 0; j < geom.CoordinateCol.Count; j++)
                        {
                            // The collection holds various coordinate types (MoveTo, LineTo, etc.).
                            // Use the base type 'Coordinate' for iteration.
                            Coordinate coord = geom.CoordinateCol[j];

                            // Identify the concrete coordinate type and print its values.
                            if (coord is MoveTo moveTo)
                            {
                                Console.WriteLine($"    Coord {j + 1} (MoveTo): X = {moveTo.X.Value}, Y = {moveTo.Y.Value}");
                            }
                            else if (coord is LineTo lineTo)
                            {
                                Console.WriteLine($"    Coord {j + 1} (LineTo): X = {lineTo.X.Value}, Y = {lineTo.Y.Value}");
                            }
                            else if (coord is ArcTo arcTo)
                            {
                                Console.WriteLine($"    Coord {j + 1} (ArcTo): X = {arcTo.X.Value}, Y = {arcTo.Y.Value}");
                            }
                            else if (coord is EllipticalArcTo ellArc)
                            {
                                Console.WriteLine($"    Coord {j + 1} (EllipticalArcTo): X = {ellArc.X.Value}, Y = {ellArc.Y.Value}");
                            }
                            else
                            {
                                // Fallback for any other coordinate types.
                                Console.WriteLine($"    Coord {j + 1} (Unknown type): {coord.GetType().Name}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}