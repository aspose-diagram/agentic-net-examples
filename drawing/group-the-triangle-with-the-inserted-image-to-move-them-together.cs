using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // -------------------------------------------------
                // 1. Create a triangle using DrawPolyline
                // -------------------------------------------------
                // Define three points of the triangle (pinX, pinY) in inches
                double[] trianglePoints = { 2.0, 2.0, 4.0, 2.0, 3.0, 4.0 };
                long triangleId = page.DrawPolyline(trianglePoints);
                Shape triangleShape = page.Shapes.GetShape(triangleId);

                // -------------------------------------------------
                // 2. Insert an image that will be grouped with the triangle
                // -------------------------------------------------
                // Adjust the position and size as needed
                using (var imageStream = new System.IO.FileStream("image.png", System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    // AddShape(pinX, pinY, width, height, Stream)
                    long imageId = page.AddShape(2.5, 2.5, 2.0, 2.0, imageStream);
                    Shape imageShape = page.Shapes.GetShape(imageId);

                    // -------------------------------------------------
                    // 3. Group the triangle and the image together
                    // -------------------------------------------------
                    // The Group method returns the new group shape
                    Shape groupShape = page.Shapes.Group(new Shape[] { triangleShape, imageShape });

                    // Optional: move the group to a new location (e.g., shift right by 1 inch)
                    groupShape.Move(1.0, 0.0);
                }

                // -------------------------------------------------
                // 4. Save the modified diagram
                // -------------------------------------------------
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }