using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output_merged.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that have no geometry or only one geometry section
                    if (shape.Geoms == null || shape.Geoms.Count <= 1)
                        continue;

                    // Create a new geometry that will hold all coordinate segments
                    Geom mergedGeom = new Geom();

                    // Copy all coordinate segments from each existing geometry
                    foreach (Geom geom in shape.Geoms)
                    {
                        // Each geometry has a collection of coordinate objects (MoveTo, LineTo, etc.)
                        foreach (var coord in geom.CoordinateCol)
                        {
                            // Add the existing coordinate object to the merged geometry
                            mergedGeom.CoordinateCol.Add((Coordinate)coord);
                        }
                    }

                    // Clear the original geometries collection
                    shape.Geoms.Clear();

                    // Add the merged geometry as the sole geometry for the shape
                    shape.Geoms.Add(mergedGeom);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Geometry merging completed. Saved to: " + outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}