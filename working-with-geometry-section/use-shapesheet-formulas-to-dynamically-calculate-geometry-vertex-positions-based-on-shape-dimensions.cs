using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a blank page to the diagram
                diagram.Pages.Add(new Page());

                // Get the first (and only) page
                Page page = diagram.Pages[0];

                // Add a rectangle master shape to the page.
                // Parameters: PinX, PinY, Width, Height, MasterName
                long shapeId = page.AddShape(2.0, 2.0, 2.0, 2.0, "Rectangle");

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Ensure the shape has at least one geometry section
                if (shape.Geoms.Count == 0)
                {
                    // If no geometry exists, add a new Geom container
                    shape.Geoms.Add(new Geom());
                }

                // Work with the first geometry (index 0)
                Geom geom = shape.Geoms[0];

                // Clear any existing geometry segments
                geom.CoordinateCol.Clear();

                // ------------------------------------------------------------
                // Define a triangle whose vertices are calculated from the shape's
                // Width and Height using ShapeSheet formulas.
                // Vertex 1 (MoveTo): (Width*0.5, 0)          – top middle
                // Vertex 2 (LineTo): (Width, Height)        – bottom right
                // Vertex 3 (LineTo): (0, Height)            – bottom left
                // Vertex 4 (LineTo): (Width*0.5, 0)          – close back to top
                // ------------------------------------------------------------

                // Vertex 1 – MoveTo
                MoveTo moveTo = new MoveTo();
                moveTo.X.Ufe.F = "Width*0.5";   // Formula for X coordinate
                moveTo.Y.Ufe.F = "0";          // Formula for Y coordinate
                geom.CoordinateCol.Add(moveTo);

                // Vertex 2 – LineTo
                LineTo lineTo1 = new LineTo();
                lineTo1.X.Ufe.F = "Width";     // X = Width
                lineTo1.Y.Ufe.F = "Height";    // Y = Height
                geom.CoordinateCol.Add(lineTo1);

                // Vertex 3 – LineTo
                LineTo lineTo2 = new LineTo();
                lineTo2.X.Ufe.F = "0";         // X = 0
                lineTo2.Y.Ufe.F = "Height";    // Y = Height
                geom.CoordinateCol.Add(lineTo2);

                // Vertex 4 – LineTo (close the shape)
                LineTo lineTo3 = new LineTo();
                lineTo3.X.Ufe.F = "Width*0.5"; // X = Width*0.5
                lineTo3.Y.Ufe.F = "0";        // Y = 0
                geom.CoordinateCol.Add(lineTo3);

                // Save the diagram to a VSDX file
                diagram.Save("DynamicTriangle.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }