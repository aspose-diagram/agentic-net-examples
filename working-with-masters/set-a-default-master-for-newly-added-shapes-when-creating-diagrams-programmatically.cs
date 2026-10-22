using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the stencil file that contains the master shapes.
        string stencilPath = "Basic_U.vssx";
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        // Output Visio diagram path.
        string outputPath = "DefaultMasterDiagram.vsdx";

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Import a master (e.g., "Rectangle") from the stencil into the diagram.
            // AddMaster returns the master ID (int); retrieve the Master object via the Masters collection.
            diagram.AddMaster(stencilPath, "Rectangle");
            Master rectangleMaster = diagram.Masters.GetMasterByName("Rectangle");

            // Store the master name to use as the default for subsequent shape additions.
            string defaultMasterName = rectangleMaster.Name;

            // Add several shapes using the default master.
            // Parameters: PinX, PinY, master name, page index (0 for the first page).
            long shapeId1 = diagram.AddShape(2.0, 2.0, defaultMasterName, 0);
            long shapeId2 = diagram.AddShape(5.0, 2.0, defaultMasterName, 0);
            long shapeId3 = diagram.AddShape(8.0, 2.0, defaultMasterName, 0);

            // Retrieve each newly added shape for optional customization.
            Shape shape1 = diagram.Pages[0].Shapes.GetShape(shapeId1);
            Shape shape2 = diagram.Pages[0].Shapes.GetShape(shapeId2);
            Shape shape3 = diagram.Pages[0].Shapes.GetShape(shapeId3);

            // Example: set simple text on each shape.
            shape1.Text.Value.Clear();
            shape1.Text.Value.Add(new Txt("Shape 1"));
            shape2.Text.Value.Clear();
            shape2.Text.Value.Add(new Txt("Shape 2"));
            shape3.Text.Value.Clear();
            shape3.Text.Value.Add(new Txt("Shape 3"));

            Console.WriteLine($"Added shapes with IDs {shapeId1}, {shapeId2}, {shapeId3} using default master '{defaultMasterName}'.");

            // Save the diagram to a VSDX file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
