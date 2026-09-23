using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source and the resulting diagram
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            Diagram diagram = new Diagram(inputPath);

            // -------------------------------------------------
            // Create a new master shape (template) programmatically
            // -------------------------------------------------
            Master customMaster = new Master();

            // Assign a unique numeric ID and a name for the master
            customMaster.ID = 1000;                     // must be unique within the diagram
            customMaster.Name = "CustomMaster";

            // Assign GUIDs required by the API
            customMaster.UniqueID = Guid.NewGuid();
            customMaster.BaseID = Guid.NewGuid();

            // Ensure the master is visible
            customMaster.Hidden = BOOL.False;

            // -------------------------------------------------
            // Define the geometry of the master by adding a shape to it
            // -------------------------------------------------
            Shape masterShape = new Shape();

            // Set the shape type to a regular 2‑D shape
            masterShape.Type = TypeValue.Shape;

            // Define size (in inches) and position of the shape within the master
            masterShape.XForm.Width.Value = 2.0;   // width
            masterShape.XForm.Height.Value = 1.0;  // height
            masterShape.XForm.PinX.Value = 1.0;    // horizontal center
            masterShape.XForm.PinY.Value = 0.5;    // vertical center

            // Optional: give the shape a simple fill color
            masterShape.Fill.FillForegnd.Value = "#FFCC00";

            // Add the shape to the master's shape collection
            customMaster.Shapes.Add(masterShape);

            // -------------------------------------------------
            // Add the new master to the diagram's master collection
            // -------------------------------------------------
            diagram.Masters.Add(customMaster);

            // -------------------------------------------------
            // Place an instance of the master on the first page
            // -------------------------------------------------
            Page page = diagram.Pages[0];

            // AddShape returns the shape ID (long). The fourth argument is a bool.
            long instanceId = page.AddShape(2.0, 2.0, customMaster.Name, false);

            // Retrieve the created shape instance if further modifications are needed
            Shape instanceShape = page.Shapes.GetShape(instanceId);

            // Example: set some text on the placed shape
            instanceShape.Text.Value.Clear();
            instanceShape.Text.Value.Add(new Txt("Hello Master"));

            // -------------------------------------------------
            // Save the modified diagram
            // -------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
