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
            // Path for the processed output file
            string outputPath = "output.vsdx";

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

                    // Create a new geometry (rectangle) matching the shape's size
                    Geom rectGeom = new Geom();

                    // Move to the origin (0,0) of the shape's local coordinate system
                    MoveTo move = new MoveTo();
                    move.X.Value = 0;
                    move.Y.Value = 0;
                    rectGeom.CoordinateCol.Add(move);

                    // Line to top‑right corner
                    LineTo line1 = new LineTo();
                    line1.X.Value = shape.XForm.Width.Value;
                    line1.Y.Value = 0;
                    rectGeom.CoordinateCol.Add(line1);

                    // Line to bottom‑right corner
                    LineTo line2 = new LineTo();
                    line2.X.Value = shape.XForm.Width.Value;
                    line2.Y.Value = shape.XForm.Height.Value;
                    rectGeom.CoordinateCol.Add(line2);

                    // Line to bottom‑left corner
                    LineTo line3 = new LineTo();
                    line3.X.Value = 0;
                    line3.Y.Value = shape.XForm.Height.Value;
                    rectGeom.CoordinateCol.Add(line3);

                    // Close the rectangle by returning to the origin
                    LineTo line4 = new LineTo();
                    line4.X.Value = 0;
                    line4.Y.Value = 0;
                    rectGeom.CoordinateCol.Add(line4);

                    // Replace existing geometry with the new rectangle geometry
                    shape.Geoms.Clear();
                    shape.Geoms.Add(rectGeom);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Processing completed. Saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
