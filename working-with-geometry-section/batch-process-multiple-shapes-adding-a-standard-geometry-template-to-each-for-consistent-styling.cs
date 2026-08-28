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
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume processing the first page; adjust index if necessary
                Page page = diagram.Pages[0];

                // Locate the template shape by its universal name (NameU)
                Shape templateShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.NameU == "TemplateShape")
                    {
                        templateShape = shp;
                        break;
                    }
                }

                if (templateShape == null)
                    throw new Exception("Template shape with NameU 'TemplateShape' not found on the page.");

                // Iterate over all shapes on the page and apply the geometry template
                foreach (Shape shp in page.Shapes)
                {
                    // Skip the template shape itself
                    if (shp.ID == templateShape.ID)
                        continue;

                    // Clear existing geometry
                    shp.Geoms.Clear();

                    // Copy geometry sections from the template shape
                    foreach (Geom tmplGeom in templateShape.Geoms)
                    {
                        // Create a new geometry section for the target shape
                        Geom newGeom = new Geom();

                        // Copy each coordinate command (MoveTo, LineTo, etc.)
                        foreach (var coord in tmplGeom.CoordinateCol)
                        {
                            // The coordinate objects can be added directly; this performs a shallow copy.
                            // For a deep copy, instantiate the specific type and copy its fields.
                            newGeom.CoordinateCol.Add(coord);
                        }

                        // Add the new geometry section to the shape
                        shp.Geoms.Add(newGeom);
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