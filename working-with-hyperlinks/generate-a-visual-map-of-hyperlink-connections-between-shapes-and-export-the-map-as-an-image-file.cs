using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Create a hub shape that will serve as the central node for hyperlink connections
                // Coordinates (5,5) are arbitrary; adjust as needed
                long hubId = page.AddShape(5.0, 5.0, "Rectangle");
                Shape hubShape = page.Shapes.GetShape(hubId);

                // Set a visible label on the hub shape
                hubShape.Text.Value.Clear();
                hubShape.Text.Value.Add(new Txt("Hyperlink Hub"));

                // Iterate over all shapes on the page
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip the hub shape itself
                    if (shape.ID == hubId)
                        continue;

                    // Check if the shape contains any hyperlinks
                    if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                    {
                        // Create a connector shape (Dynamic connector master)
                        long connectorId = page.AddShape(0.0, 0.0, "Dynamic connector");
                        Shape connector = page.Shapes.GetShape(connectorId);

                        // Connect the shape to the hub using the connector
                        page.ConnectShapesViaConnector(
                            shape.ID,                     // source shape ID
                            ConnectionPointPlace.Bottom, // source connection point
                            hubId,                       // target shape ID (hub)
                            ConnectionPointPlace.Top,    // target connection point
                            connectorId);                // connector shape ID

                        // Set arrowheads for better visual indication
                        connector.Line.BeginArrow.Value = 4; // Arrow style
                        connector.Line.EndArrow.Value = 4;   // Arrow style
                    }
                }

                // Prepare image save options for PNG export
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0;   // Export first page
                saveOptions.PageCount = 1;   // Single page
                saveOptions.Resolution = 300f; // Optional: higher resolution

                // Save the visual map as an image file
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Hyperlink connection map saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}