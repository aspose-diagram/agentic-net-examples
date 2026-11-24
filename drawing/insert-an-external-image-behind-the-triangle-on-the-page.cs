using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        // External image file path to insert
        string imagePath = "background.png";
        // Output file path (PNG format)
        string outputPath = "output.png";

        // Guard against missing diagram file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard against missing image file
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Work with the first page
                Page page = diagram.Pages[0];

                // Define position and size for the background image (in inches)
                double imgPinX = 1.0;   // X coordinate of the image center
                double imgPinY = 1.0;   // Y coordinate of the image center
                double imgWidth = 5.0;  // Width of the image
                double imgHeight = 5.0; // Height of the image

                // Insert the external image as a shape
                long imgShapeId;
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    imgShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, fs);
                }

                // Retrieve the image shape and send it to the back
                Shape imgShape = page.Shapes.GetShape(imgShapeId);
                imgShape.SendToBack();

                // Define triangle vertices (in inches) and close the polygon
                double[] trianglePoints = new double[]
                {
                    2.0, 2.0,   // Vertex 1
                    4.0, 2.0,   // Vertex 2
                    3.0, 4.0,   // Vertex 3
                    2.0, 2.0    // Closing back to Vertex 1
                };

                // Draw the triangle on the same page (will appear above the background image)
                page.DrawPolyline(trianglePoints);

                // Save the modified diagram as PNG
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}