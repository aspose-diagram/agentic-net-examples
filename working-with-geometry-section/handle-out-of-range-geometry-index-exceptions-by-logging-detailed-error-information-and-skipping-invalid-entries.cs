using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example index that may be out of range
                        int targetGeomIndex = 5;

                        try
                        {
                            // Attempt to retrieve the geometry at the specified index
                            Geom geom = (Geom)shape.Geoms[targetGeomIndex];

                            // If successful, perform any required processing here
                            Console.WriteLine($"Page {pageIndex + 1}, Shape ID {shape.ID}: Geometry {targetGeomIndex} accessed successfully.");
                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            // Log detailed error information and skip this geometry
                            Console.WriteLine($"[Error] Page {pageIndex + 1}, Shape ID {shape.ID}, Geometry index {targetGeomIndex} is out of range.");
                            Console.WriteLine($"Message: {ex.Message}");
                            Console.WriteLine($"StackTrace: {ex.StackTrace}");
                            // Continue with the next shape
                        }
                        catch (Exception ex)
                        {
                            // Log any other unexpected errors
                            Console.WriteLine($"[Unexpected Error] Page {pageIndex + 1}, Shape ID {shape.ID}, Geometry index {targetGeomIndex}.");
                            Console.WriteLine($"Message: {ex.Message}");
                        }
                    }
                }

                // Save the diagram (no modifications made in this example)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }