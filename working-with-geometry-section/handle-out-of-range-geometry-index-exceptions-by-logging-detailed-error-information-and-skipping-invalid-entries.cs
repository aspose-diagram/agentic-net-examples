using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_processed.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through pages, shapes, and geometry sections
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];
                        long shapeId = shape.ID;

                        // Iterate over each Geom collection in the shape
                        for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                        {
                            // The Geoms collection returns objects; cast to Geom
                            Geom geom = (Geom)shape.Geoms[geomIndex];

                            // Iterate over the coordinate collection safely
                            for (int coordIndex = 0; coordIndex < geom.CoordinateCol.Count; coordIndex++)
                            {
                                try
                                {
                                    // Access the geometry segment; the exact type (MoveTo, LineTo, etc.) is not required here
                                    object segment = geom.CoordinateCol[coordIndex];
                                    // Example operation: just output the segment type
                                    Console.WriteLine($"Page {pageIndex}, Shape ID {shapeId}, Geom {geomIndex}, Coord {coordIndex}: {segment.GetType().Name}");
                                }
                                catch (IndexOutOfRangeException ex)
                                {
                                    // Log detailed error information and continue with the next entry
                                    Console.WriteLine($"[ERROR] Out-of-range geometry index encountered:");
                                    Console.WriteLine($"  Page Index   : {pageIndex}");
                                    Console.WriteLine($"  Shape ID     : {shapeId}");
                                    Console.WriteLine($"  Geom Index   : {geomIndex}");
                                    Console.WriteLine($"  Coord Index  : {coordIndex}");
                                    Console.WriteLine($"  Exception    : {ex.Message}");
                                    // Skip this invalid entry and continue
                                }
                                catch (Exception ex)
                                {
                                    // Catch any other unexpected exceptions, log, and continue
                                    Console.WriteLine($"[ERROR] Unexpected exception while processing geometry:");
                                    Console.WriteLine($"  Page Index   : {pageIndex}");
                                    Console.WriteLine($"  Shape ID     : {shapeId}");
                                    Console.WriteLine($"  Geom Index   : {geomIndex}");
                                    Console.WriteLine($"  Coord Index  : {coordIndex}");
                                    Console.WriteLine($"  Exception    : {ex.GetType().Name} - {ex.Message}");
                                }
                            }
                        }
                    }
                }

                // Save the processed diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processing complete. Saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }