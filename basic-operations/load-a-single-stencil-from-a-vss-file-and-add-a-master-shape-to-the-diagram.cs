using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the stencil (.vss) file
        string stencilPath = "stencil.vss";
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        // Path where the resulting diagram will be saved
        string outputPath = "output.vsdx";

        // Guard: ensure the stencil file exists
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Name of the master to import from the stencil
            string masterName = "Rectangle";

            // Import the master shape from the stencil file into the diagram
            diagram.AddMaster(stencilPath, masterName);

            // Coordinates where the shape will be placed on the first page (page index 0)
            double pinX = 2.0;
            double pinY = 2.0;

            // Add the shape to the diagram using the imported master
            // The AddShape method returns the shape ID (long)
            long shapeId = diagram.AddShape(pinX, pinY, masterName, 0);

            // (Optional) Retrieve the shape object if further modifications are needed
            // Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Save the diagram to a VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}