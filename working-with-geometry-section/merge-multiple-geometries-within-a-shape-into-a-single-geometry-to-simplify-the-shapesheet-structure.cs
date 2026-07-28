using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that have no geometry sections
                        if (shape.Geoms == null || shape.Geoms.Count <= 1)
                            continue;

                        // Keep the first geometry as the target
                        Geom targetGeom = shape.Geoms[0];

                        // Merge geometry from subsequent Geom objects into the target
                        for (int i = 1; i < shape.Geoms.Count; i++)
                        {
                            Geom sourceGeom = shape.Geoms[i];

                            // Append each coordinate segment from the source geometry to the target geometry
                            foreach (var segment in sourceGeom.CoordinateCol)
                            {
                                // Add the segment to the target geometry's coordinate collection
                                targetGeom.CoordinateCol.Add(segment);
                            }

                            // Mark the source geometry as deleted
                            sourceGeom.Del = BOOL.True;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Geometry merge completed. Saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }