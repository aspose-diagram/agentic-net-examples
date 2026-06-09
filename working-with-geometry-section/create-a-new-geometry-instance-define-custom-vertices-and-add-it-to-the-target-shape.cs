using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve a target shape by its ID (example ID = 1)
                Shape shape = page.Shapes.GetShape(1);

                // Create a new geometry (Geom) instance
                Geom customGeom = new Geom();

                // Define the starting point of the geometry
                MoveTo start = new MoveTo();
                start.X.Value = 0.5; // X coordinate in inches
                start.Y.Value = 0.5; // Y coordinate in inches
                customGeom.CoordinateCol.Add(start);

                // Define custom vertices using LineTo objects
                LineTo vertex1 = new LineTo();
                vertex1.X.Value = 2.0;
                vertex1.Y.Value = 0.5;
                customGeom.CoordinateCol.Add(vertex1);

                LineTo vertex2 = new LineTo();
                vertex2.X.Value = 2.0;
                vertex2.Y.Value = 2.0;
                customGeom.CoordinateCol.Add(vertex2);

                LineTo vertex3 = new LineTo();
                vertex3.X.Value = 0.5;
                vertex3.Y.Value = 2.0;
                customGeom.CoordinateCol.Add(vertex3);

                // Close the shape by returning to the start point
                LineTo close = new LineTo();
                close.X.Value = 0.5;
                close.Y.Value = 0.5;
                customGeom.CoordinateCol.Add(close);

                // Add the custom geometry to the shape's Geoms collection
                shape.Geoms.Add(customGeom);

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }