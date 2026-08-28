using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the shape to modify (example: shape with ID = 1)
                // Replace the ID with the actual shape ID you need to update
                int shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                    throw new Exception($"Shape with ID {shapeId} not found.");

                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count == 0)
                    throw new Exception("The shape does not contain any geometry sections.");

                // Get the first geometry (index 0) and cast to Geom
                Geom targetGeom = (Geom)shape.Geoms[0];

                // Disable (delete) existing vertex segments by marking them as deleted
                foreach (var segment in targetGeom.CoordinateCol)
                {
                    // All segment types inherit the Del property (BOOL)
                    segment.Del = BOOL.True;
                }

                // Define new dimensions for the shape (in inches)
                double newWidth = 2.0;   // example width
                double newHeight = 1.0;  // example height

                // Build a new rectangle geometry: MoveTo (0,0) -> LineTo (newWidth,0) -> LineTo (newWidth,newHeight)
                // -> LineTo (0,newHeight) -> LineTo (0,0) to close the path

                // MoveTo (starting point)
                MoveTo move = new MoveTo();
                move.X.Value = 0.0;
                move.Y.Value = 0.0;
                targetGeom.CoordinateCol.Add(move);

                // LineTo (right side)
                LineTo line1 = new LineTo();
                line1.X.Value = newWidth;
                line1.Y.Value = 0.0;
                targetGeom.CoordinateCol.Add(line1);

                // LineTo (top side)
                LineTo line2 = new LineTo();
                line2.X.Value = newWidth;
                line2.Y.Value = newHeight;
                targetGeom.CoordinateCol.Add(line2);

                // LineTo (left side)
                LineTo line3 = new LineTo();
                line3.X.Value = 0.0;
                line3.Y.Value = newHeight;
                targetGeom.CoordinateCol.Add(line3);

                // LineTo (close the rectangle)
                LineTo line4 = new LineTo();
                line4.X.Value = 0.0;
                line4.Y.Value = 0.0;
                targetGeom.CoordinateCol.Add(line4);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }