using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <imagePath> <outputDiagramPath>");
            return;
        }

        string inputDiagramPath = args[0];
        string imagePath = args[1];
        string outputDiagramPath = args[2];

        // Guard against missing files.
        if (!File.Exists(inputDiagramPath))
        {
            Console.Error.WriteLine($"Input diagram file not found: {inputDiagramPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputDiagramPath))
            {
                // Read the image bytes that will be embedded into the master.
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Create a new master that will hold the data graphic (bitmap image).
                Master dataGraphicMaster = new Master();

                // Assign a unique numeric ID for the master.
                dataGraphicMaster.ID = diagram.Masters.Count + 1;

                // Set basic master metadata.
                dataGraphicMaster.Name = "DataGraphicMaster";
                dataGraphicMaster.UniqueID = Guid.NewGuid();
                dataGraphicMaster.BaseID = Guid.NewGuid();
                dataGraphicMaster.Hidden = BOOL.False;
                dataGraphicMaster.MatchByName = BOOL.True;
                dataGraphicMaster.IconUpdate = BOOL.True;

                // Create a shape that will represent the embedded image.
                Shape imageShape = new Shape();

                // Define the shape as a foreign (image) shape.
                imageShape.Type = TypeValue.Foreign;

                // Set the desired size of the image (in inches).
                imageShape.XForm.Width.Value = 2.0;   // Width
                imageShape.XForm.Height.Value = 1.0;  // Height

                // Embed the bitmap data into the shape.
                imageShape.ForeignData.Value = imageBytes;
                imageShape.ForeignData.ForeignType = ForeignType.Bitmap;
                imageShape.ForeignData.CompressionType = CompressionType.PNG;

                // Add the shape to the master's shape collection.
                dataGraphicMaster.Shapes.Add(imageShape);

                // Add the master to the diagram's master collection.
                diagram.Masters.Add(dataGraphicMaster);

                // Ensure there is at least one page to place the shape on.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page (index 0) for demonstration.
                int pageIndex = 0;
                Page page = diagram.Pages[pageIndex];

                // Add a shape based on the newly created master.
                // This overload places the shape using the master name.
                long shapeId = diagram.AddShape(5.0, 5.0, dataGraphicMaster.Name, pageIndex);

                // Retrieve the shape object to optionally set additional properties.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape != null)
                {
                    // Add a custom property that could be used by a data graphic.
                    Prop prop = new Prop();
                    prop.Label.Value = "SampleValue";
                    prop.Value.Val = "123";
                    shape.Props.Add(prop);
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputDiagramPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}