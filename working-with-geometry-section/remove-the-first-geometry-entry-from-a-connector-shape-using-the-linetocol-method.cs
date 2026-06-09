using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Find the first connector shape (1-D shape)
                Shape connector = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.OneD) // Connectors are 1-D shapes
                    {
                        connector = shape;
                        break;
                    }
                }

                if (connector == null)
                {
                    Console.WriteLine("No connector shape found in the diagram.");
                    return;
                }

                // Ensure the connector has at least one geometry section
                if (connector.Geoms.Count == 0)
                {
                    Console.WriteLine("Connector has no geometry sections.");
                    return;
                }

                // Access the first geometry (Geom) and cast it explicitly
                Geom firstGeom = (Geom)connector.Geoms[0];

                // Ensure there is at least one LineTo entry in the geometry
                if (firstGeom.CoordinateCol.LineToCol.Count == 0)
                {
                    Console.WriteLine("No LineTo entries found in the first geometry.");
                    return;
                }

                // Remove (disable) the first LineTo entry by setting its Del flag to TRUE
                LineTo firstLineTo = firstGeom.CoordinateCol.LineToCol[0];
                firstLineTo.Del = BOOL.True;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Connector geometry updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }