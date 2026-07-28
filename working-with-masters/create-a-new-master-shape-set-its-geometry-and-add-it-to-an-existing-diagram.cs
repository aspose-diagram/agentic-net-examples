using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Create a new master
                Master customMaster = new Master();
                customMaster.ID = diagram.Masters.Count + 1;               // Unique numeric ID
                customMaster.Name = "CustomMaster";                        // Master name
                customMaster.UniqueID = Guid.NewGuid();                    // GUID for the master
                customMaster.BaseID = Guid.NewGuid();                      // Base GUID

                // Create a shape that will be the visual content of the master
                Shape masterShape = new Shape();
                masterShape.ID = 1;                                        // Shape ID within the master
                masterShape.Name = "MasterShape";
                masterShape.Type = TypeValue.Shape;                        // Set shape type

                // Define size of the shape (2 inches width, 1 inch height)
                masterShape.XForm.Width.Value = 2.0;
                masterShape.XForm.Height.Value = 1.0;

                // Build geometry for a simple rectangle
                // Geometry consists of a MoveTo (starting point) followed by LineTo segments
                Geom geom = new Geom();
                // Starting point at (0,0)
                MoveTo moveTo = new MoveTo();
                moveTo.X.Value = 0.0;
                moveTo.Y.Value = 0.0;
                geom.CoordinateCol.Add(moveTo);
                // Line to (width,0)
                LineTo line1 = new LineTo();
                line1.X.Value = masterShape.XForm.Width.Value;
                line1.Y.Value = 0.0;
                geom.CoordinateCol.Add(line1);
                // Line to (width,height)
                LineTo line2 = new LineTo();
                line2.X.Value = masterShape.XForm.Width.Value;
                line2.Y.Value = masterShape.XForm.Height.Value;
                geom.CoordinateCol.Add(line2);
                // Line to (0,height)
                LineTo line3 = new LineTo();
                line3.X.Value = 0.0;
                line3.Y.Value = masterShape.XForm.Height.Value;
                geom.CoordinateCol.Add(line3);
                // Close the rectangle by returning to the start point
                LineTo line4 = new LineTo();
                line4.X.Value = 0.0;
                line4.Y.Value = 0.0;
                geom.CoordinateCol.Add(line4);

                // Add the geometry to the shape
                masterShape.Geoms.Add(geom);

                // Add the shape to the master
                customMaster.Shapes.Add(masterShape);

                // Add the master to the diagram's master collection
                diagram.Masters.Add(customMaster);

                // Place an instance of the new master on the first page
                Page page = diagram.Pages[0];
                // PinX and PinY define the position of the shape on the page
                double pinX = 2.0;
                double pinY = 2.0;
                long shapeId = page.AddShape(pinX, pinY, customMaster.Name);
                // Retrieve the created shape if further modifications are needed
                Shape instanceShape = page.Shapes.GetShape(shapeId);

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