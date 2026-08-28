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

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (and only) page
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // 1. Draw a triangle using DrawPolyline
                // -------------------------------------------------
                // Coordinates (in inches) for the three vertices
                double x1 = 2.0, y1 = 2.0;
                double x2 = 4.0, y2 = 2.0;
                double x3 = 3.0, y3 = 4.0;

                // Close the polygon by repeating the first point at the end
                long triangleId = page.DrawPolyline(new double[]
                {
                    x1, y1,
                    x2, y2,
                    x3, y3,
                    x1, y1
                });

                // Retrieve the triangle shape object
                Shape triangleShape = page.Shapes.GetShape(triangleId);

                // Optional: give the triangle a name for easier debugging
                triangleShape.Name = "MyTriangle";

                // -------------------------------------------------
                // 2. Insert an image onto the page
                // -------------------------------------------------
                // Path to the image file (ensure the file exists)
                string imagePath = "sample.png";

                // Define where the image will be placed and its size
                double imgPinX = 2.5;   // center X
                double imgPinY = 2.5;   // center Y
                double imgWidth = 2.0;
                double imgHeight = 2.0;

                // Add the image as a foreign shape using a FileStream
                long imageId;
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // The overload AddShape(pinX, pinY, width, height, Stream) inserts the image
                    imageId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, fs);
                }

                // Retrieve the image shape object
                Shape imageShape = page.Shapes.GetShape(imageId);
                imageShape.Name = "MyImage";

                // -------------------------------------------------
                // 3. Group the triangle and the image together
                // -------------------------------------------------
                // The Group method expects an array of Shape objects
                Shape groupShape = page.Shapes.Group(new Shape[] { triangleShape, imageShape });

                // Optional: give the group a name
                groupShape.Name = "TriangleImageGroup";

                // -------------------------------------------------
                // 4. Save the diagram to a VSDX file
                // -------------------------------------------------
                string outputPath = "GroupedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved to '{outputPath}'. The triangle and image are now grouped.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }