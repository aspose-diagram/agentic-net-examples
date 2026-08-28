using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the actual file path
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all shapes in the diagram
                foreach (Shape shape in diagram.Pages[0].Shapes)
                {
                    // Each shape may contain multiple Geom objects (paths)
                    GeomCollection geoms = shape.Geoms;
                    for (int g = 0; g < geoms.Count; g++)
                    {
                        Geom geom = geoms[g];
                        // Coordinate collection holds the actual vertex definitions
                        CoordinateCollection coords = geom.CoordinateCol;

                        // Log MoveTo vertices
                        foreach (MoveTo move in coords.MoveToCol)
                        {
                            Console.WriteLine($"Shape ID {shape.ID}, Geom {g}, MoveTo: X={move.X.Value}, Y={move.Y.Value}");
                        }

                        // Log LineTo vertices
                        foreach (LineTo line in coords.LineToCol)
                        {
                            Console.WriteLine($"Shape ID {shape.ID}, Geom {g}, LineTo: X={line.X.Value}, Y={line.Y.Value}");
                        }

                        // Log PolylineTo vertices
                        foreach (PolylineTo poly in coords.PolylineToCol)
                        {
                            Console.WriteLine($"Shape ID {shape.ID}, Geom {g}, PolylineTo: X={poly.X.Value}, Y={poly.Y.Value}, A={poly.A.Value}");
                        }

                        // Log ArcTo vertices
                        foreach (ArcTo arc in coords.ArcToCol)
                        {
                            Console.WriteLine($"Shape ID {shape.ID}, Geom {g}, ArcTo: X={arc.X.Value}, Y={arc.Y.Value}, A={arc.A.Value}");
                        }

                        // Add additional coordinate types as needed (e.g., RelLineTo, RelMoveTo, etc.)
                    }
                }

                // Optionally save the diagram if modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }