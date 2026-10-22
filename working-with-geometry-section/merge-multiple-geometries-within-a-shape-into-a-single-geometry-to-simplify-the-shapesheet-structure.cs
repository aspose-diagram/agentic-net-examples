using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path
        string outputPath = "output_merged.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

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
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only shapes that have more than one geometry
                    if (shape.Geoms != null && shape.Geoms.Count > 1)
                    {
                        // Use the first geometry as the target for merging
                        var targetGeom = shape.Geoms[0];

                        // Merge geometry rows from subsequent geometries into the target
                        for (int g = 1; g < shape.Geoms.Count; g++)
                        {
                            var srcGeom = shape.Geoms[g];

                            // Ensure the source geometry has coordinate rows
                            if (srcGeom.CoordinateCol != null)
                            {
                                // Copy each coordinate row to the target geometry
                                foreach (var row in srcGeom.CoordinateCol)
                                {
                                    // Add the row to the target geometry's collection
                                    targetGeom.CoordinateCol.Add(row);
                                }
                            }
                        }

                        // Remove the now-merged extra geometries (from last to first to keep indices valid)
                        for (int g = shape.Geoms.Count - 1; g > 0; g--)
                        {
                            shape.Geoms.RemoveAt(g);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}