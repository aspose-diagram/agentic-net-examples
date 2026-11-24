using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: master file path, master name inside the file, output diagram path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <masterFilePath> <masterName> <outputDiagramPath>");
            return;
        }

        string masterFilePath = args[0];
        string masterName = args[1];
        string outputDiagramPath = args[2];

        // Guard: ensure the master file exists.
        if (!File.Exists(masterFilePath))
        {
            Console.Error.WriteLine($"Master file not found: {masterFilePath}");
            return;
        }

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Import the user‑defined master from the specified stencil file.
            // The AddMaster method loads the master so it can be used later.
            diagram.AddMaster(masterFilePath, masterName);

            // Ensure the diagram has at least five pages (pages are zero‑based indexed).
            while (diagram.Pages.Count < 5)
            {
                // Add a new blank page.
                diagram.Pages.Add(new Page());
            }

            // Retrieve page five (index 4).
            Page pageFive = diagram.Pages[4];

            // Add a shape based on the imported master to page five.
            // PinX and PinY are arbitrary coordinates; adjust as needed.
            double pinX = 2.0;
            double pinY = 2.0;
            // The fourth parameter indicates whether to calculate geometry; false for simple placement.
            long shapeId = pageFive.AddShape(pinX, pinY, masterName, false);

            // Retrieve the newly added shape using its ID.
            Shape shape = pageFive.Shapes.GetShape(shapeId);

            // Set the shape's text label.
            // First clear any existing text.
            shape.Text.Value.Clear();
            // Add the desired text.
            shape.Text.Value.Add(new Txt("Custom Shape Label"));

            // Save the resulting diagram to the specified output path in VSDX format.
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}