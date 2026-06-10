using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Load an existing diagram (replace with actual file path)
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            // Create a new master
            Master newMaster = new Master
            {
                ID = 1000, // ensure unique within the diagram
                Name = "CustomMaster",
                NameU = "CustomMaster",
                UniqueID = Guid.NewGuid(),
                BaseID = Guid.NewGuid(),
                Hidden = BOOL.False,
                MatchByName = BOOL.True,
                IconUpdate = BOOL.True
            };

            // Create a shape that will belong to the master
            Shape masterShape = new Shape
            {
                Type = TypeValue.Shape
            };

            // Define the shape size and position within the master
            masterShape.XForm.Width.Value = 2.0;   // width in inches
            masterShape.XForm.Height.Value = 1.0;  // height in inches
            masterShape.XForm.PinX.Value = 1.0;    // center X
            masterShape.XForm.PinY.Value = 0.5;    // center Y

            // Build simple rectangular geometry for the master shape
            Geom geom = new Geom();

            MoveTo move = new MoveTo();
            move.X.Value = 0.0;
            move.Y.Value = 0.0;
            geom.CoordinateCol.Add(move);

            LineTo line1 = new LineTo();
            line1.X.Value = 2.0;
            line1.Y.Value = 0.0;
            geom.CoordinateCol.Add(line1);

            LineTo line2 = new LineTo();
            line2.X.Value = 2.0;
            line2.Y.Value = 1.0;
            geom.CoordinateCol.Add(line2);

            LineTo line3 = new LineTo();
            line3.X.Value = 0.0;
            line3.Y.Value = 1.0;
            geom.CoordinateCol.Add(line3);

            LineTo line4 = new LineTo();
            line4.X.Value = 0.0;
            line4.Y.Value = 0.0;
            geom.CoordinateCol.Add(line4);

            // Attach geometry to the shape
            masterShape.Geoms.Add(geom);

            // Add the shape to the master's shape collection
            newMaster.Shapes.Add(masterShape);

            // Add the new master to the diagram's master collection
            diagram.Masters.Add(newMaster);

            // Place an instance of the new master on the active page
            Page page = diagram.ActivePage;
            long instanceShapeId = page.AddShape(5.0, 5.0, newMaster.Name, false);

            // (Optional) Retrieve the instance shape if further modifications are needed
            Shape instanceShape = page.Shapes.GetShape(instanceShapeId);

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}