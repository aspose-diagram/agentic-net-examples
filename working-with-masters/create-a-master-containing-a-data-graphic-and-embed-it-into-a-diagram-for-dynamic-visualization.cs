using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // 1. Create a new empty diagram.
            Diagram diagram = new Diagram();

            // 2. Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // 3. Create a custom master that will act as a data graphic container.
            Master dataGraphicMaster = new Master
            {
                // Assign a unique numeric ID.
                ID = diagram.Masters.Count + 1,
                // Give the master a recognizable name.
                Name = "DataGraphicMaster",
                // Unique identifiers for the master.
                UniqueID = Guid.NewGuid(),
                BaseID = Guid.NewGuid()
            };

            // 4. Define a simple rectangle shape inside the master.
            Shape masterShape = new Shape
            {
                // Shape type must be a regular shape.
                Type = TypeValue.Shape
            };
            // Set geometry directly on the existing XForm instance.
            masterShape.XForm.PinX.Value = 2.0;   // X position (in inches)
            masterShape.XForm.PinY.Value = 2.0;   // Y position (in inches)
            masterShape.XForm.Width.Value = 3.0;  // Width (in inches)
            masterShape.XForm.Height.Value = 1.5; // Height (in inches)

            // Add placeholder text that could be bound to data later.
            masterShape.Text.Value.Add(new Txt("<<Data>>"));

            // Add the shape to the master's shape collection.
            dataGraphicMaster.Shapes.Add(masterShape);

            // 5. Add the custom master to the diagram's master collection.
            diagram.Masters.Add(dataGraphicMaster);

            // 6. Place an instance of the master onto the first page.
            Page page = diagram.Pages[0];

            // AddShape returns a long identifier for the new shape.
            long instanceId = page.AddShape(
                pinX: 5.0,          // X position on the page (in inches)
                pinY: 5.0,          // Y position on the page (in inches)
                width: 3.0,         // Width of the shape (in inches)
                height: 1.5,        // Height of the shape (in inches)
                masterName: dataGraphicMaster.Name,
                isCalculate: false // Do not recalculate geometry automatically.
            );

            // Retrieve the shape instance to modify its properties if needed.
            Shape instanceShape = page.Shapes.GetShape(instanceId);

            // Example: set a different fill color for the instance.
            instanceShape.Fill.FillForegnd.Value = "#DDEEFF";

            // Example: set line color and weight.
            instanceShape.Line.LineColor.Value = "#336699";
            instanceShape.Line.LineWeight.Value = 0.02; // inches

            // 7. Save the diagram to a VSDX file.
            string outputPath = "DataGraphicDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}