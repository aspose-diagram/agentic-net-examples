using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first page (created by default)
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Insert an image onto the page
            // -----------------------------------------------------------------
            // Image file path (adjust as needed)
            string imagePath = "image.png";

            // Position and size for the image (in inches)
            double imgPinX = 2.0;   // center X
            double imgPinY = 2.0;   // center Y
            double imgWidth = 4.0;
            double imgHeight = 3.0;

            // Add the image shape using a FileStream
            long imgShapeId;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                imgShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, fs);
            }

            // Retrieve the image shape (optional, e.g., to set protection)
            Shape imgShape = page.Shapes.GetShape(imgShapeId);
            // Make the background image non‑selectable
            imgShape.Protection.LockSelect.Value = BOOL.True;
            // Send the image to back so other shapes appear above it
            imgShape.SendToBack();

            // -----------------------------------------------------------------
            // Draw a triangle positioned above the image
            // -----------------------------------------------------------------
            // Calculate triangle coordinates based on the image size
            double imageTop = imgPinY + imgHeight / 2.0;          // top edge of the image
            double gap = 0.2;                                    // small gap between image and triangle
            double baseY = imageTop + gap;                       // Y coordinate of the triangle base
            double triHeight = 2.0;                              // height of the triangle
            double apexY = baseY + triHeight;                    // Y coordinate of the triangle apex
            double leftX = imgPinX - imgWidth / 2.0;             // left X of the image (and triangle base)
            double rightX = imgPinX + imgWidth / 2.0;            // right X of the image (and triangle base)
            double centerX = imgPinX;                            // center X for the apex

            // Points: left base, right base, apex, back to left base to close the shape
            double[] trianglePoints = new double[]
            {
                leftX,  baseY,   // left base
                rightX, baseY,   // right base
                centerX, apexY,  // apex
                leftX,  baseY    // close polygon
            };

            // Draw the triangle (returns the shape ID)
            long triShapeId = page.DrawPolyline(trianglePoints);

            // Retrieve the triangle shape to apply formatting
            Shape triShape = page.Shapes.GetShape(triShapeId);
            // Fill color (red) and line color (black)
            triShape.Fill.FillForegnd.Value = "#FF0000";
            triShape.Line.LineColor.Value = "#000000";

            // -----------------------------------------------------------------
            // Save the diagram
            // -----------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
