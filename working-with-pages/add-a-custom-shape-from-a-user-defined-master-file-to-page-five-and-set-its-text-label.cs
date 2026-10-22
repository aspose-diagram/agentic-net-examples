using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments:
        // 0 - path to the target diagram file (existing Visio file)
        // 1 - path to the user‑defined master (stencil) file
        // 2 - name of the master inside the stencil to use
        // 3 - output diagram file path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <diagramPath> <masterFilePath> <masterName> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string masterFilePath = args[1];
        string masterName = args[2];
        string outputPath = args[3];

        // Guard input files
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(masterFilePath))
        {
            Console.Error.WriteLine($"Master file not found: {masterFilePath}");
            return;
        }

        try
        {
            // Load the existing diagram
            Diagram diagram = new Diagram(diagramPath);

            // Import the custom master from the user‑defined stencil
            // This adds the master to the diagram's Masters collection
            diagram.AddMaster(masterFilePath, masterName);

            // Ensure the diagram has at least five pages (page index 4)
            while (diagram.Pages.Count < 5)
            {
                // Add a new blank page
                diagram.Pages.Add(new Page());
            }

            // Retrieve page five (zero‑based index 4)
            Page pageFive = diagram.Pages[4];

            // Add a shape based on the imported master to page five
            // PinX and PinY are set to 1.0; adjust as needed
            long shapeId = pageFive.AddShape(1.0, 1.0, masterName, false);

            // Retrieve the newly added shape object
            Shape shape = pageFive.Shapes.GetShape(shapeId);

            // Set the shape's text label
            shape.Text.Value.Clear();                     // Remove any existing text
            shape.Text.Value.Add(new Txt("Custom Label")); // Add new text

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}