using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to a Visio stencil (.vss or .vssx) that contains the desired master.
        // Replace with an actual file path when running the code.
        string stencilPath = @"C:\Stencils\Basic_U.vssx";

        // Guard: ensure the stencil file exists before proceeding.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }

        // Name of the master shape to use as the default for new shapes.
        string defaultMasterName = "Rectangle";

        // Create a new empty diagram.
        Diagram diagram = new Diagram();

        try
        {
            // Ensure there is at least one page to work with.
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Import the master from the stencil into the diagram.
            // AddMaster returns the master ID; we ignore it here.
            diagram.AddMaster(stencilPath, defaultMasterName);

            // Retrieve the first page (index 0).
            Page page = diagram.Pages[0];

            // Add a shape using the default master.
            // The AddShape method returns the shape ID (long).
            long shapeId = page.AddShape(2.0, 2.0, 1.5, 1.0, defaultMasterName);

            // Retrieve the shape object to modify its properties if needed.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example: set some text on the newly added shape.
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Default master shape"));

            // Save the diagram to a VSDX file.
            string outputPath = "DefaultMasterDiagram.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Propagate any errors; in a real application you might log this.
            throw new Exception("An error occurred while creating the diagram: " + ex.Message, ex);
        }
    }
}