using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (lifecycle rule)
            Diagram diagram = new Diagram();

            // Use the first page of the diagram
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Insert an image onto the page
            // -----------------------------------------------------------------
            // Image file path (adjust as needed)
            const string imagePath = "image.png";

            // Define image position and size (in inches)
            double imgPinX = 5.0;   // X‑coordinate of the image centre
            double imgPinY = 5.0;   // Y‑coordinate of the image centre
            double imgWidth = 2.0; // Image width
            double imgHeight = 2.0; // Image height

            // Add the image shape using the stream overload (AddShape overload)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                long imageShapeId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
                // imageShapeId can be used later if needed
            }

            // -----------------------------------------------------------------
            // Add a triangle shape positioned above the image
            // -----------------------------------------------------------------
            // Define triangle position (above the image)
            double trianglePinX = imgPinX;          // Same X as the image
            double trianglePinY = imgPinY + imgHeight / 2 + 0.5; // Slight gap above the image

            // Master name for a triangle shape (Visio built‑in)
            const string triangleMaster = "Triangle";

            // Add the triangle shape (AddShape overload with master name)
            long triangleShapeId = page.AddShape(trianglePinX, trianglePinY, triangleMaster);
            // triangleShapeId can be used later if needed

            // -----------------------------------------------------------------
            // Save the diagram (lifecycle rule)
            // -----------------------------------------------------------------
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
