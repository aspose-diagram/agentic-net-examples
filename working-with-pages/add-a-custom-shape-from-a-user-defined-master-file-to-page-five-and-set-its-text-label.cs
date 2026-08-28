using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram path (must exist)
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Master (stencil) file containing the custom shape (must exist)
        string masterFilePath = "custom_master.vssx";
        if (!File.Exists(masterFilePath))
        {
            Console.Error.WriteLine($"File not found: {masterFilePath}");
            return;
        }

        // Name of the master (shape) inside the stencil file
        string masterName = "MyCustomShape";

        // Output Visio diagram path
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Import the custom master from the stencil into the diagram
            diagram.AddMaster(masterFilePath, masterName);

            // Ensure the diagram has at least five pages
            if (diagram.Pages.Count < 5)
            {
                Console.Error.WriteLine("The diagram does not contain a fifth page.");
                return;
            }

            // Retrieve page five (zero‑based index 4)
            Page page = diagram.Pages[4];

            // Add a shape based on the imported master to page five
            // Coordinates (PinX, PinY) are set to 2.0 inches; isCalculate = false
            long shapeId = page.AddShape(2.0, 2.0, masterName, false);

            // Obtain the Shape object using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text runs
            shape.Text.Value.Clear();

            // Add the desired text label to the shape
            shape.Text.Value.Add(new Txt("Custom Shape Label"));
            
            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}