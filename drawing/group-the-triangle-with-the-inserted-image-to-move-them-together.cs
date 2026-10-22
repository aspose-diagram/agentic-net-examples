using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - path to the source Visio diagram (e.g., Drawing1.vsdx)
        // args[1] - path to the image file to insert (e.g., picture.png)
        // args[2] - path to save the modified diagram (e.g., Modified.vsdx)
        // args[3] - path to export the diagram as PNG (e.g., Output.png)

        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <imagePath> <outputDiagramPath> <outputPngPath>");
            return;
        }

        string diagramPath = args[0];
        string imagePath = args[1];
        string outputDiagramPath = args[2];
        string outputPngPath = args[3];

        // Guard against missing input files
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
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // ------------------------------------------------------------
            // Insert the image as a shape on the page
            // ------------------------------------------------------------
            // Define position and size for the image (in inches)
            double imgPinX = 1.0;   // X coordinate of the shape's pin
            double imgPinY = 1.0;   // Y coordinate of the shape's pin
            double imgWidth = 2.0; // Width of the image
            double imgHeight = 2.0; // Height of the image

            long imageShapeId;
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // AddShape returns the shape's ID (long)
                imageShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
            }

            // Retrieve the Shape object for further operations
            Shape imageShape = page.Shapes.GetShape(imageShapeId);

            // ------------------------------------------------------------
            // Draw a triangle shape on the same page
            // ------------------------------------------------------------
            // Define triangle vertices (in inches) and close the polygon by repeating the first point
            double[] trianglePoints = new double[]
            {
                3.0, 3.0,   // Vertex 1
                5.0, 3.0,   // Vertex 2
                4.0, 5.0,   // Vertex 3
                3.0, 3.0    // Close the triangle
            };

            // DrawPolyline returns the shape's ID (long)
            long triangleShapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the Shape object for the triangle
            Shape triangleShape = page.Shapes.GetShape(triangleShapeId);

            // ------------------------------------------------------------
            // Group the image shape and the triangle shape together
            // ------------------------------------------------------------
            Shape groupShape = page.Shapes.Group(new Shape[] { imageShape, triangleShape });

            // ------------------------------------------------------------
            // Move the group as a single entity (e.g., shift 1 inch right and 1 inch up)
            // ------------------------------------------------------------
            groupShape.Move(1.0, 1.0);

            // ------------------------------------------------------------
            // Save the modified diagram in VSDX format
            // ------------------------------------------------------------
            diagram.Save(outputDiagramPath, SaveFileFormat.Vsdx);

            // ------------------------------------------------------------
            // Export the diagram (first page) as a PNG image
            // ------------------------------------------------------------
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPngPath, pngOptions);

            Console.WriteLine("Diagram processing completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}