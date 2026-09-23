using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Uniform scaling factor (e.g., 2.0 doubles the size)
        double scaleFactor = 2.0;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may contain one or more geometry sections
                    foreach (Geom geom in shape.Geoms)
                    {
                        // Iterate over all coordinate objects in the geometry
                        for (int i = 0; i < geom.CoordinateCol.Count; i++)
                        {
                            var coord = geom.CoordinateCol[i];

                            // Scale MoveTo vertices
                            if (coord is MoveTo move)
                            {
                                move.X.Value *= scaleFactor;
                                move.Y.Value *= scaleFactor;
                            }
                            // Scale LineTo vertices
                            else if (coord is LineTo line)
                            {
                                line.X.Value *= scaleFactor;
                                line.Y.Value *= scaleFactor;
                            }
                            // Scale ArcTo vertices (if present)
                            else if (coord is ArcTo arc)
                            {
                                arc.X.Value *= scaleFactor;
                                arc.Y.Value *= scaleFactor;
                                // Scale the arc's radius parameter 'A' if needed
                                arc.A.Value *= scaleFactor;
                                // Note: 'B' property does not exist in this version; omitted to avoid compile error
                            }
                            // Additional coordinate types can be handled here as required
                        }
                    }
                }
            }

            // Path for the scaled output diagram
            string outputPath = "output.vsdx";

            // Save the modified diagram using the correct overload
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}