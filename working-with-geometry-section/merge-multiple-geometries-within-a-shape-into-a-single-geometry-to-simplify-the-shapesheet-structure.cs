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
                string outputPath = "output_merged.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a Geoms collection with more than one geometry
                        if (shape.Geoms != null && shape.Geoms.Count > 1)
                        {
                            // Take the first geometry as the target for merging
                            Geom targetGeom = shape.Geoms[0];

                            // Merge all subsequent geometries into the target geometry
                            for (int i = 1; i < shape.Geoms.Count; i++)
                            {
                                Geom sourceGeom = shape.Geoms[i];

                                // Append each segment from the source geometry to the target geometry
                                foreach (var segment in sourceGeom.CoordinateCol)
                                {
                                    targetGeom.CoordinateCol.Add(segment);
                                }

                                // Mark the source geometry as deleted to hide it
                                sourceGeom.Del = BOOL.True;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }