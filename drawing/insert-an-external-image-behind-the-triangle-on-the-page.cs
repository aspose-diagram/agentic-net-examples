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

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the first triangle shape on the page
                Shape triangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangle = shape;
                        break;
                    }
                }

                if (triangle == null)
                {
                    Console.WriteLine("Triangle shape not found on the page.");
                    return;
                }

                // Path to the external image to be inserted
                const string imagePath = "background.png";

                // Insert the image as a shape positioned behind the triangle
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // Use the triangle's position and size for the image shape
                    double pinX = triangle.XForm.PinX.Value;
                    double pinY = triangle.XForm.PinY.Value;
                    double width = triangle.XForm.Width.Value;
                    double height = triangle.XForm.Height.Value;

                    // Add the image shape; AddShape returns the shape ID (long)
                    long imageShapeId = page.AddShape(pinX, pinY, width, height, imgStream);

                    // Retrieve the shape object to modify its Z-order
                    Shape imageShape = page.Shapes.GetShape(imageShapeId);
                    imageShape.SendToBack(); // Ensure the image is behind other shapes
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Image inserted behind the triangle and diagram saved as output.vsdx.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
