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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the new dimensions for the shape (in inches)
            double newWidth = 2.0;
            double newHeight = 1.0;

            // Find the target shape by its universal name (adjust as needed)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                throw new Exception("Target shape not found.");
            }

            // Ensure the shape has at least one geometry section
            if (targetShape.Geoms.Count == 0)
            {
                throw new Exception("Shape does not contain any geometry.");
            }

            // Get the first geometry (Geom) of the shape
            Geom geom = (Geom)targetShape.Geoms[0];

            // Clear existing coordinate collection
            geom.CoordinateCol.Clear();

            // Build a rectangle geometry with the new dimensions
            // 1. MoveTo (0,0) – starting point
            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            // 2. LineTo (newWidth,0)
            LineTo line1 = new LineTo();
            line1.X.Value = newWidth;
            line1.Y.Value = 0.0;
            geom.CoordinateCol.Add(line1);

            // 3. LineTo (newWidth,newHeight)
            LineTo line2 = new LineTo();
            line2.X.Value = newWidth;
            line2.Y.Value = newHeight;
            geom.CoordinateCol.Add(line2);

            // 4. LineTo (0,newHeight)
            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = newHeight;
            geom.CoordinateCol.Add(line3);

            // 5. Close the shape by returning to the start point (0,0)
            LineTo line4 = new LineTo();
            line4.X.Value = 0.0;
            line4.Y.Value = 0.0;
            geom.CoordinateCol.Add(line4);

            // Optionally, update the shape's width and height cells to match the new size
            targetShape.XForm.Width.Value = newWidth;
            targetShape.XForm.Height.Value = newHeight;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
