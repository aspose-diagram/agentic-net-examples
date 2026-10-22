using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define file paths and parameters.
        string diagramPath = "input.vsdx";          // Path to the source Visio diagram.
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string stencilPath = "stencil.vssx";        // Path to the stencil file containing the master.
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        string masterName = "Rectangle";            // Name of the master to add from the stencil.
        double pinX = 2.0;                          // X coordinate where the shape will be placed.
        double pinY = 2.0;                          // Y coordinate where the shape will be placed.
        string outputPath = "output.vsdx";          // Path for the resulting diagram.

        // Guard against missing input files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Import the master from the stencil file.
                // This makes the master available for shape creation.
                diagram.AddMaster(stencilPath, masterName);

                // Retrieve the active page (first page) where the shape will be added.
                Page page = diagram.ActivePage;

                // Add a new shape based on the imported master.
                // The fourth argument 'false' indicates that the shape should not be recalculated immediately.
                long shapeId = page.AddShape(pinX, pinY, masterName, false);

                // Optionally retrieve the shape object if further modifications are needed.
                // Shape shape = page.Shapes.GetShape(shapeId);
                // (No additional modifications in this example.)

                // Save the modified diagram to the specified output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Master shape '{masterName}' added and diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}