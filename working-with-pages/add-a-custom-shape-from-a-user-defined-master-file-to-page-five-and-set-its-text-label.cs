using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the existing Visio file (or create a new one)
            string diagramPath = "input.vsdx";
            // Path to the master (stencil) file that contains the custom shape
            string masterFilePath = "CustomMaster.vssx";
            // Name of the master (shape) to import from the stencil
            string masterName = "MyCustomShape";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram has at least five pages
            while (diagram.Pages.Count < 5)
            {
                // Add a blank page; the constructor assigns a new ID automatically
                diagram.Pages.Add(new Page());
            }

            // Import the custom master from the user‑defined master file
            // This makes the master available for shape creation
            diagram.AddMaster(masterFilePath, masterName);

            // Page index for page five (zero‑based index = 4)
            int pageIndex = 4;
            Page page = diagram.Pages[pageIndex];

            // Add the custom shape to page five
            // PinX and PinY are the coordinates where the shape will be placed
            double pinX = 2.0;
            double pinY = 2.0;
            long shapeId = diagram.AddShape(pinX, pinY, masterName, pageIndex);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the text label of the shape
            shape.Text.Value.Clear();                         // Remove any existing text runs
            shape.Text.Value.Add(new Txt("Custom Shape Label")); // Add new text

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
