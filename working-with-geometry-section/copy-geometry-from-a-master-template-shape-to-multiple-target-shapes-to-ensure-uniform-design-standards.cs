using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Name of the shape that serves as the geometry template
            const string templateShapeName = "TemplateShape";

            // Retrieve the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the template shape on the page
            Shape templateShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == templateShapeName)
                {
                    templateShape = shape;
                    break;
                }
            }

            if (templateShape == null)
            {
                throw new Exception($"Template shape '{templateShapeName}' not found on page '{page.Name}'.");
            }

            // Iterate over all shapes on the page and copy geometry from the template
            foreach (Shape targetShape in page.Shapes)
            {
                // Skip the template shape itself
                if (targetShape.ID == templateShape.ID)
                    continue;

                // Remove any existing geometry from the target shape
                targetShape.Geoms.Clear();

                // Clone each geometry element from the template and add it to the target shape
                foreach (Geom geom in templateShape.Geoms)
                {
                    // Clone returns an object; cast it back to Geom
                    Geom clonedGeom = (Geom)geom.Clone();
                    targetShape.Geoms.Add(clonedGeom);
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
