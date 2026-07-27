using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a new page (the diagram starts with one page by default)
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // 1. Draw a triangle using DrawPolyline.
                //    The points are defined in a flat double array.
                //    The triangle is closed by repeating the first point.
                // -------------------------------------------------
                double[] trianglePoints = new double[]
                {
                    2.0, 2.0,   // Point 1 (PinX, PinY)
                    5.0, 2.0,   // Point 2
                    3.5, 5.0,   // Point 3
                    2.0, 2.0    // Close the polygon
                };
                long triangleId = page.DrawPolyline(trianglePoints);
                Shape triangleShape = page.Shapes.GetShape(triangleId);

                // -------------------------------------------------
                // 2. Insert an image as a shape.
                //    The image file is read into a FileStream and added to the page.
                // -------------------------------------------------
                const string imagePath = "image.png"; // Ensure this file exists in the working directory
                if (!File.Exists(imagePath))
                    throw new FileNotFoundException($"Image file not found: {imagePath}");

                // Position and size for the image shape (in inches)
                double imgPinX = 4.0;
                double imgPinY = 3.0;
                double imgWidth = 3.0;
                double imgHeight = 2.0;

                long imageId;
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    imageId = page.AddShape(imgPinX, imgPinY, imgWidth, imgHeight, imgStream);
                }
                Shape imageShape = page.Shapes.GetShape(imageId);

                // -------------------------------------------------
                // 3. Group the triangle and the image so they move together.
                // -------------------------------------------------
                Shape groupShape = page.Shapes.Group(new Shape[] { triangleShape, imageShape });

                // Optional: move the whole group to a new location
                groupShape.Move(1.0, 1.0); // Move 1 inch right and 1 inch up

                // -------------------------------------------------
                // 4. Save the diagram to a VSDX file.
                // -------------------------------------------------
                string outputPath = "GroupedDiagram.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }