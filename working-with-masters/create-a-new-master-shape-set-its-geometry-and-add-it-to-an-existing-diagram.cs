using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a new master shape from scratch
                // ------------------------------------------------------------
                Master newMaster = new Master();

                // Set master identifiers
                newMaster.NameU = "CustomRect";
                newMaster.Name = "CustomRect";

                // Optional master properties
                newMaster.Hidden = BOOL.False;
                newMaster.MatchByName = BOOL.True;
                newMaster.IconUpdate = BOOL.True;

                // Create a shape that will define the visual appearance of the master
                Shape masterShape = new Shape();

                // Set geometry for the master shape (2 cm width, 1 cm height)
                // Width, Height, PinX, PinY expect double values (in inches). Using direct double literals.
                masterShape.XForm.Width.Value = 2.0;   // width
                masterShape.XForm.Height.Value = 1.0;  // height
                masterShape.XForm.PinX.Value = 0.0;    // pin X
                masterShape.XForm.PinY.Value = 0.0;    // pin Y

                // Add the shape to the master's shape collection
                newMaster.Shapes.Add(masterShape);

                // Add the master to the diagram's master collection
                diagram.Masters.Add(newMaster);

                // ------------------------------------------------------------
                // Add an instance of the newly created master to page 0
                // ------------------------------------------------------------
                double pinX = 5.0; // 5 cm from the left
                double pinY = 5.0; // 5 cm from the top

                // AddShape returns a long identifier for the new shape
                long shapeId = diagram.AddShape(pinX, pinY, 2.0, 1.0, newMaster.NameU, 0);

                // Retrieve the shape object from the first page's shape collection
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                if (shape == null)
                {
                    Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                    return;
                }

                // Set some visual properties on the shape instance
                shape.Fill.FillForegnd.Value = "#FF5733"; // orange fill
                shape.Line.LineColor.Value = "#000000";   // black border

                // Set shape text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Hello Master"));

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
