using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Access the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Find the triangle shape on the page
            Shape? triangleShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Triangle")
                {
                    triangleShape = shape;
                    break;
                }
            }

            if (triangleShape == null)
            {
                Console.WriteLine("Triangle shape not found on the page.");
                return;
            }

            // Get triangle geometry to position the background image
            double pinX = triangleShape.XForm.PinX.Value;
            double pinY = triangleShape.XForm.PinY.Value;
            double width = triangleShape.XForm.Width.Value;
            double height = triangleShape.XForm.Height.Value;

            // Insert external image as a shape
            string imagePath = "background.png";
            long imageShapeId;
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // AddShape(pinX, pinY, width, height, stream) adds an image shape
                imageShapeId = page.AddShape(pinX, pinY, width, height, imgStream);
            }

            // Retrieve the newly added image shape
            Shape imageShape = page.Shapes.GetShape((int)imageShapeId);

            // Send the image to back so the triangle appears on top
            imageShape.SendToBack();

            // Optional: make the background image non‑selectable
            imageShape.Protection.LockSelect.Value = BOOL.True;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
