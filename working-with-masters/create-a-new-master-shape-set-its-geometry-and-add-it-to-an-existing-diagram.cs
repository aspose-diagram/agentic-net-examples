using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a new master shape from scratch.
                // ------------------------------------------------------------
                Master customMaster = new Master();

                // Assign a unique numeric ID for the master.
                customMaster.ID = diagram.Masters.Count + 1;

                // Set master identifiers.
                customMaster.NameU = "CustomRect";
                customMaster.Name = "CustomRect";

                // Assign GUIDs for uniqueness.
                customMaster.UniqueID = Guid.NewGuid();
                customMaster.BaseID = Guid.NewGuid();

                // Ensure the master is visible and name‑matching is enabled.
                customMaster.Hidden = BOOL.False;
                customMaster.MatchByName = BOOL.True;
                customMaster.IconUpdate = BOOL.True;

                // ------------------------------------------------------------
                // Define the visual geometry of the master by adding a shape.
                // ------------------------------------------------------------
                Shape masterShape = new Shape();

                // Set the shape type to a regular shape.
                masterShape.Type = TypeValue.Shape;

                // Define geometry: width = 2 inches, height = 1 inch.
                masterShape.XForm.Width.Value = 2.0;
                masterShape.XForm.Height.Value = 1.0;

                // Position the shape at the origin of the master.
                masterShape.XForm.PinX.Value = 0.0;
                masterShape.XForm.PinY.Value = 0.0;

                // Add the shape to the master's shape collection.
                customMaster.Shapes.Add(masterShape);

                // ------------------------------------------------------------
                // Add the new master to the diagram's master collection.
                // ------------------------------------------------------------
                diagram.Masters.Add(customMaster);

                // ------------------------------------------------------------
                // Add an instance of the newly created master to the first page.
                // ------------------------------------------------------------
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the overload that specifies position, size, master name and page index.
                long shapeId = diagram.AddShape(
                    5.0,          // X position on the page
                    5.0,          // Y position on the page
                    2.0,          // Width of the shape instance
                    1.0,          // Height of the shape instance
                    customMaster.NameU,
                    0);           // First page (index 0)

                // Retrieve the concrete Shape object.
                Shape shapeInstance = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Optional: set some text on the shape to verify it was added.
                shapeInstance.Text.Value.Clear();
                shapeInstance.Text.Value.Add(new Txt("Custom Master Shape"));

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
