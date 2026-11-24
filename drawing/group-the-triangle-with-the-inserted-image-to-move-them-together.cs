using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <imagePath> <outputDiagramPath> <outputPngPath>");
            return;
        }

        string diagramPath = args[0];
        string imagePath = args[1];
        string outputDiagramPath = args[2];
        string outputPngPath = args[3];

        // Guard against missing input files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Obtain the first page, or create a new one if the diagram is empty.
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    // Create a new empty page and add it to the diagram.
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // ------------------------------------------------------------
                // Insert the external image as a shape.
                // ------------------------------------------------------------
                double imgPinX = 2.0;   // X coordinate of the shape's pin (in inches)
                double imgPinY = 2.0;   // Y coordinate of the shape's pin (in inches)
                double imgWidth = 4.0;  // Width of the image (in inches)
                double imgHeight = 3.0; // Height of the image (in inches)

                long imageShapeId;
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape returns the shape ID (long).
                    imageShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
                }
                Shape imageShape = page.Shapes.GetShape(imageShapeId);

                // ------------------------------------------------------------
                // Draw a triangle using DrawPolyline.
                // ------------------------------------------------------------
                // Define triangle vertices (in inches). The last point repeats the first to close the shape.
                double[] trianglePoints = new double[]
                {
                    5.0, 5.0,   // Vertex 1
                    7.0, 5.0,   // Vertex 2
                    6.0, 7.0,   // Vertex 3
                    5.0, 5.0    // Close back to Vertex 1
                };
                long triangleShapeId = page.DrawPolyline(trianglePoints);
                Shape triangleShape = page.Shapes.GetShape(triangleShapeId);

                // ------------------------------------------------------------
                // Group the triangle and the image together.
                // ------------------------------------------------------------
                Shape groupShape = page.Shapes.Group(new Shape[] { triangleShape, imageShape });

                // ------------------------------------------------------------
                // Move the group as a single entity.
                // ------------------------------------------------------------
                // Move the group 2 inches to the right and 1 inch up.
                groupShape.Move(2.0, -1.0);

                // ------------------------------------------------------------
                // Save the modified diagram.
                // ------------------------------------------------------------
                diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

                // ------------------------------------------------------------
                // Export the first page as PNG to verify the result.
                // ------------------------------------------------------------
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPngPath, pngOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}