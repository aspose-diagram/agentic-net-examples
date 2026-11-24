using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the stencil file that contains the master shape.
        string stencilPath = "Basic_U.vssx";
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        // Name of the master to be used as the default for new shapes.
        string defaultMasterName = "Rectangle";

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Import the master from the stencil into the diagram.
            // This makes the master available for shape creation.
            diagram.AddMaster(stencilPath, defaultMasterName);

            // Add a new shape using the default master.
            // The fourth parameter (isCalculate) must be a bool, not an int.
            long shapeId = diagram.ActivePage.AddShape(5.0, 5.0, defaultMasterName, false);

            // Retrieve the newly added shape for further customization.
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Example customization: set the shape's fill foreground color.
            shape.Fill.FillForegnd.Value = "#FFCC00"; // orange fill

            // Save the diagram to a VSDX file.
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}