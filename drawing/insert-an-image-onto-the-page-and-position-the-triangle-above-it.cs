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

            // Create a new diagram instance
            using (Diagram diagram = new Diagram())
            {
                // Get the first page (creates one if the diagram is empty)
                Page page = diagram.Pages[0];

                // ---------- Insert image ----------
                long imageShapeId;
                // Open the image file as a stream
                using (FileStream imgStream = new FileStream("image.png", FileMode.Open, FileAccess.Read))
                {
                    // Add the image as a shape at (2,2) inches with size 4x3 inches
                    imageShapeId = page.AddShape(2.0, 2.0, 4.0, 3.0, imgStream);
                }
                // Retrieve the shape object and send it to the back so other shapes appear above it
                Shape imageShape = page.Shapes.GetShape((int)imageShapeId);
                imageShape.SendToBack();

                // ---------- Draw triangle ----------
                // Define triangle vertices and close the shape by repeating the first point
                // Points: (3,5) -> (5,5) -> (4,7) -> (3,5)
                long triangleShapeId = page.DrawPolyline(new double[] { 3, 5, 5, 5, 4, 7, 3, 5 });
                Shape triangle = page.Shapes.GetShape((int)triangleShapeId);

                // Position the triangle above the image
                // Align horizontally with the image center (PinX = 2.0)
                // Place vertically above the image (PinY = 5.0)
                triangle.XForm.PinX.Value = 2.0;
                triangle.XForm.PinY.Value = 5.0;

                // Optional: set a fill color for the triangle
                triangle.Fill.FillForegnd.Value = "#FF0000"; // Red

                // ---------- Save diagram ----------
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
