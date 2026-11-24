using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "Drawing1.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        // Input image file path to be inserted
        string imagePath = "image.png";
        // Output file path (PNG format)
        string outputPath = "Result.png";

        // Guard clauses for input files
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
            // Load the Visio diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // -----------------------------------------------------------------
                // Insert the image onto the page
                // -----------------------------------------------------------------
                // Define image position and size (in inches)
                double imgPinX = 2.0;   // X coordinate of the lower-left corner
                double imgPinY = 2.0;   // Y coordinate of the lower-left corner
                double imgWidth = 4.0;  // Width of the image
                double imgHeight = 3.0; // Height of the image

                // Add the image shape using a FileStream
                long imageShapeId;
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // page.AddShape returns the shape ID (long)
                    imageShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
                }

                // Retrieve the inserted image shape for further manipulation (e.g., send to back)
                Shape imageShape = page.Shapes.GetShape(imageShapeId);
                // Ensure the image stays behind other shapes
                imageShape.SendToBack();

                // -----------------------------------------------------------------
                // Draw a triangle above the inserted image
                // -----------------------------------------------------------------
                // Define triangle vertices (coordinates in inches)
                // Position the triangle so its base aligns with the top edge of the image
                double baseY = imgPinY + imgHeight; // top edge of the image
                double offsetX = imgPinX;            // align left edge with image
                double triangleWidth = imgWidth;    // same width as image
                double triangleHeight = 2.0;        // height of the triangle

                double x1 = offsetX;                         // left corner of base
                double y1 = baseY;                           // base left
                double x2 = offsetX + triangleWidth;         // right corner of base
                double y2 = baseY;                           // base right
                double x3 = offsetX + triangleWidth / 2.0;   // apex (centered)
                double y3 = baseY + triangleHeight;          // apex height

                // Draw the triangle as a closed polyline (repeat first point at the end)
                page.DrawPolyline(new double[]
                {
                    x1, y1,
                    x2, y2,
                    x3, y3,
                    x1, y1 // close the shape
                });

                // -----------------------------------------------------------------
                // Save the modified diagram as a PNG image
                // -----------------------------------------------------------------
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}