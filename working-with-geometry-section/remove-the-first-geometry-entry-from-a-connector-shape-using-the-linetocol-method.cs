using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            bool modified = false;

            // Iterate through pages and shapes to find a connector (1‑D shape)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes; the OneD property is a native bool
                    if (shape.OneD)
                    {
                        // Ensure the shape has at least one geometry section
                        if (shape.Geoms.Count > 0)
                        {
                            // Retrieve the first geometry (cast required)
                            Geom geom = (Geom)shape.Geoms[0];

                            // Ensure there is at least one LineTo entry in the geometry
                            if (geom.CoordinateCol.LineToCol.Count > 0)
                            {
                                // Mark the first LineTo segment as deleted
                                LineTo firstLineTo = geom.CoordinateCol.LineToCol[0];
                                firstLineTo.Del = BOOL.True;

                                modified = true;
                                // Exit loops after the first modification
                                break;
                            }
                        }
                    }
                }
                if (modified) break;
            }

            if (!modified)
            {
                Console.WriteLine("No suitable connector geometry found to modify.");
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
