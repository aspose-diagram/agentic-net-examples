using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: <imagePath> <outputDiagramPath>
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <imagePath> <outputDiagramPath>");
            return;
        }

        string imagePath = args[0];
        string outputPath = args[1];

        // Guard: ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    // Add a new page (the default constructor creates an empty page)
                    diagram.Pages.Add(new Page());
                }

                // -----------------------------------------------------------------
                // Create a custom master that will be used to host the data graphic
                // -----------------------------------------------------------------
                Master master = new Master();
                master.Name = "DataGraphicMaster";
                // Assign a unique ID (long) to the master
                master.ID = 1;

                // Add the master to the diagram's master collection
                diagram.Masters.Add(master);

                // ---------------------------------------------------------------
                // Add a shape on the first page using the newly created master
                // ---------------------------------------------------------------
                // PinX and PinY define the position of the shape on the page
                double pinX = 5.0;
                double pinY = 5.0;
                // Page index 0 refers to the first page
                long shapeId = diagram.AddShape(pinX, pinY, master.Name, 0);

                // Retrieve the shape object using the returned ID
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // ---------------------------------------------------------------
                // Embed the image (data graphic) into the shape as foreign data
                // ---------------------------------------------------------------
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                // The ForeignData.Value property accepts a byte array representing the bitmap
                shape.ForeignData.Value = imageBytes;

                // ---------------------------------------------------------------
                // Save the diagram to VSDX format
                // ---------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}