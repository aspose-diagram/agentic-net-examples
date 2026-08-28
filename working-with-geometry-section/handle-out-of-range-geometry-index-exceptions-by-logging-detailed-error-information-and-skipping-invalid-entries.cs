using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the processed output file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Process each page and its shapes
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    var page = diagram.Pages[pageIndex];

                    foreach (Shape shape in page.Shapes)
                    {
                        // Iterate through the geometry sections of the shape
                        for (int geomIndex = 0; geomIndex < shape.Geoms.Count; geomIndex++)
                        {
                            try
                            {
                                // Retrieve the geometry object
                                var geom = (Geom)shape.Geoms[geomIndex];

                                // Example operation: iterate over coordinate collection
                                for (int coordIndex = 0; coordIndex < geom.CoordinateCol.Count; coordIndex++)
                                {
                                    var segment = geom.CoordinateCol[coordIndex];
                                    // Placeholder for any geometry manipulation logic
                                    // e.g., segment.X.Value = segment.X.Value + 0.1;
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                // Log detailed error information and skip the invalid geometry entry
                                Console.WriteLine($"[Warning] Page {pageIndex + 1}, Shape ID {shape.ID}, Geometry index {geomIndex} is out of range. Details: {ex.Message}");
                                continue;
                            }
                            catch (Exception ex)
                            {
                                // Log unexpected errors without halting the entire processing
                                Console.WriteLine($"[Error] Unexpected exception on Page {pageIndex + 1}, Shape ID {shape.ID}, Geometry index {geomIndex}. Details: {ex}");
                                continue;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }