using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram (uses the standard load lifecycle)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Remove any existing geometry definitions
                    shape.Geoms.Clear();

                    // Create a new geometry that represents a rectangle matching the shape's size
                    // Move to the origin (0,0)
                    MoveTo move = new MoveTo();
                    move.X.Value = 0;
                    move.Y.Value = 0;

                    // Line to the top‑right corner
                    LineTo line1 = new LineTo();
                    line1.X.Value = shape.XForm.Width.Value;
                    line1.Y.Value = 0;

                    // Line to the bottom‑right corner
                    LineTo line2 = new LineTo();
                    line2.X.Value = shape.XForm.Width.Value;
                    line2.Y.Value = shape.XForm.Height.Value;

                    // Line to the bottom‑left corner
                    LineTo line3 = new LineTo();
                    line3.X.Value = 0;
                    line3.Y.Value = shape.XForm.Height.Value;

                    // Close the rectangle by returning to the origin
                    LineTo line4 = new LineTo();
                    line4.X.Value = 0;
                    line4.Y.Value = 0;

                    // Assemble the geometry segment collection
                    Geom geom = new Geom();
                    geom.CoordinateCol.Add(move);
                    geom.CoordinateCol.Add(line1);
                    geom.CoordinateCol.Add(line2);
                    geom.CoordinateCol.Add(line3);
                    geom.CoordinateCol.Add(line4);

                    // Assign the new geometry to the shape
                    shape.Geoms.Add(geom);
                }
            }

            // Path for the updated Visio file
            string outputPath = "output.vsdx";

            // Save the modified diagram using the standard save lifecycle
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
