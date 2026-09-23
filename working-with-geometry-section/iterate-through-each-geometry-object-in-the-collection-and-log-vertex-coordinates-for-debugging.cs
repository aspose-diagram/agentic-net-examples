using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Each shape may contain one or more geometry sections
                        foreach (Geom geom in shape.Geoms)
                        {
                            // The CoordinateCol collection holds drawing commands (MoveTo, LineTo, etc.)
                            foreach (object segment in geom.CoordinateCol)
                            {
                                // Log MoveTo vertices
                                if (segment is MoveTo move)
                                {
                                    Console.WriteLine($"Shape ID {shape.ID} - MoveTo: X={move.X.Value}, Y={move.Y.Value}");
                                }
                                // Log LineTo vertices
                                else if (segment is LineTo line)
                                {
                                    Console.WriteLine($"Shape ID {shape.ID} - LineTo: X={line.X.Value}, Y={line.Y.Value}");
                                }
                                // Other segment types (e.g., ArcTo, EllipticalArcTo) can be added similarly
                            }
                        }
                    }
                }

                // Optional: Save the diagram if any modifications were made
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }