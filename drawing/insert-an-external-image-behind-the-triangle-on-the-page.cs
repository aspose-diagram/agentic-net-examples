using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string diagramPath = "input.vsdx";
            string imagePath = "background.png";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Access the first page (modify if a different page is required)
                Page page = diagram.Pages[0];

                // Locate the triangle shape on the page
                Shape triangle = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == "Triangle")
                    {
                        triangle = shape;
                        break;
                    }
                }

                if (triangle == null)
                {
                    throw new Exception("Triangle shape not found on the page.");
                }

                // Retrieve the triangle's position and size
                double pinX = triangle.XForm.PinX.Value;
                double pinY = triangle.XForm.PinY.Value;
                double width = triangle.XForm.Width.Value;
                double height = triangle.XForm.Height.Value;

                // Insert the external image as a shape behind the triangle
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    long imgShapeId = page.AddShape(pinX, pinY, width, height, imgStream);
                    Shape imgShape = page.Shapes.GetShape(imgShapeId);
                    imgShape.SendToBack(); // Ensure the image is behind other shapes
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
