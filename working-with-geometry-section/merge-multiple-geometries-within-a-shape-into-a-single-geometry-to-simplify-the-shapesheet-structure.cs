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
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that contain multiple geometry sections
                    if (shape.Geoms.Count > 1)
                    {
                        // Use the first geometry as the primary target for merging
                        Geom primaryGeom = (Geom)shape.Geoms[0];

                        // Loop through the remaining geometries (secondary)
                        for (int i = 1; i < shape.Geoms.Count; i++)
                        {
                            Geom secondaryGeom = (Geom)shape.Geoms[i];

                            // Append each coordinate (MoveTo, LineTo, etc.) from the secondary geometry
                            foreach (object coord in secondaryGeom.CoordinateCol)
                            {
                                // The collection expects a Coordinate instance; cast accordingly
                                primaryGeom.CoordinateCol.Add((Coordinate)coord);
                            }

                            // Mark the secondary geometry as deleted to hide it
                            secondaryGeom.Del = BOOL.True;
                        }
                    }
                }
            }

            // Output Visio file path
            string outputPath = "merged_output.vsdx";

            // Save the modified diagram using the appropriate SaveFileFormat enum
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}