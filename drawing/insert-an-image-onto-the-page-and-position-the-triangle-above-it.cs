using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "Drawing1.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Image file to insert (second argument or default)
        string imagePath = args.Length > 1 ? args[1] : "image.png";
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Output PNG path (third argument or default)
        string outputPath = args.Length > 2 ? args[2] : "output.png";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // ------------------------------------------------------------
            // Insert the image onto the page
            // ------------------------------------------------------------
            double imgPinX = 2.0;   // X coordinate of lower-left corner
            double imgPinY = 2.0;   // Y coordinate of lower-left corner
            double imgWidth = 4.0; // Width of the image
            double imgHeight = 3.0; // Height of the image

            long imgShapeId;
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // AddShape returns the shape ID (long)
                imgShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
            }

            // Retrieve the shape object to adjust Z-order
            Shape imgShape = page.Shapes.GetShape(imgShapeId);
            imgShape.SendToBack(); // Ensure the image stays behind other shapes

            // ------------------------------------------------------------
            // Draw a triangle positioned above the inserted image
            // ------------------------------------------------------------
            // Calculate triangle points: base aligns with top of image, apex above it
            double triBaseY = imgPinY + imgHeight;          // Y coordinate of image top
            double triApexY = triBaseY + 2.0;               // Apex placed 2 inches above the base
            double triLeftX = imgPinX;                      // Left base X
            double triRightX = imgPinX + imgWidth;          // Right base X

            double[] trianglePoints = new double[]
            {
                triLeftX,  triBaseY,                     // Left base point
                triRightX, triBaseY,                     // Right base point
                (triLeftX + triRightX) / 2.0, triApexY, // Apex point
                triLeftX,  triBaseY                      // Closing point (repeat first)
            };

            // DrawPolyline returns the shape ID of the created polyline
            long triShapeId = page.DrawPolyline(trianglePoints);
            Shape triShape = page.Shapes.GetShape(triShapeId);
            // No additional styling applied; triangle will appear above the image

            // ------------------------------------------------------------
            // Save the modified diagram as a PNG image
            // ------------------------------------------------------------
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram successfully saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}