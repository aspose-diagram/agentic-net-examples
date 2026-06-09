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

            // Paths for input Visio file and output image
            string inputPath = "input.vsdx";
            string outputPath = "output.png";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to modify (example uses shape ID 1)
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Ensure the shape contains geometry data
            if (shape.Geoms.Count == 0)
            {
                Console.WriteLine("Shape has no geometry to modify.");
                return;
            }

            // Retrieve the first geometry section
            Geom geom = (Geom)shape.Geoms[0];

            // Desired new dimensions (in inches)
            double newWidth = 2.0;
            double newHeight = 1.0;

            // Calculate scaling factors based on the shape's current size
            double origWidth = shape.XForm.Width.Value;
            double origHeight = shape.XForm.Height.Value;
            double scaleX = newWidth / origWidth;
            double scaleY = newHeight / origHeight;

            // Update each vertex coordinate in the geometry
            foreach (var segment in geom.CoordinateCol)
            {
                if (segment is MoveTo move)
                {
                    move.X.Value *= scaleX;
                    move.Y.Value *= scaleY;
                }
                else if (segment is LineTo line)
                {
                    line.X.Value *= scaleX;
                    line.Y.Value *= scaleY;
                }
                // Additional segment types (ArcTo, etc.) can be handled similarly if required
            }

            // Adjust the shape's bounding box to the new dimensions
            shape.XForm.Width.Value = newWidth;
            shape.XForm.Height.Value = newHeight;

            // Save the modified diagram as a PNG image
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine("Geometry updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
