using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Ensure there is at least one page (default diagram has one)
                Page page = diagram.Pages[0];

                // Define desired position for the new shape
                double targetPinX = 2.0; // inches
                double targetPinY = 2.0; // inches

                // Add a rectangle shape to the first page.
                // The master name "Rectangle" is a standard Visio master.
                // The fourth parameter is the page index (0‑based).
                long shapeId = diagram.AddShape(targetPinX, targetPinY, "Rectangle", 0);

                // Retrieve the concrete Shape object using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Verify that the shape's position matches the requested coordinates
                double actualPinX = shape.XForm.PinX.Value;
                double actualPinY = shape.XForm.PinY.Value;
                const double tolerance = 0.0001;

                if (Math.Abs(actualPinX - targetPinX) > tolerance || Math.Abs(actualPinY - targetPinY) > tolerance)
                {
                    throw new Exception($"Shape placement verification failed. Expected PinX={targetPinX}, PinY={targetPinY} but got PinX={actualPinX}, PinY={actualPinY}.");
                }
                else
                {
                    Console.WriteLine($"Shape placed correctly at PinX={actualPinX}, PinY={actualPinY}.");
                }

                // Retrieve geometry information
                int geomCount = shape.Geoms.Count;
                Console.WriteLine($"Shape contains {geomCount} geometry section(s).");

                // Example: output the first coordinate of the first geometry (if any)
                if (geomCount > 0)
                {
                    // Each Geom has a CoordinateCol collection of drawing commands (MoveTo, LineTo, etc.)
                    var firstGeom = shape.Geoms[0];
                    int coordCount = firstGeom.CoordinateCol.Count;
                    Console.WriteLine($"First geometry has {coordCount} coordinate command(s).");

                    if (coordCount > 0)
                    {
                        // Show the type of the first command
                        var firstCommand = firstGeom.CoordinateCol[0];
                        Console.WriteLine($"First command type: {firstCommand.GetType().Name}");
                    }
                }

                // Optional: save the diagram to verify visually (not required by the task)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }