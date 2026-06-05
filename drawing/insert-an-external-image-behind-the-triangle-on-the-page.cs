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

                // Paths to the Visio file and the external image.
                string diagramPath = "input.vsdx";
                string imagePath = "background.png";
                string outputPath = "output.vsdx";

                // Load the existing diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Assume we work with the first page.
                Page page = diagram.Pages[0];

                // Get page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Insert the image as a shape that covers the whole page.
                // PinX and PinY represent the center of the shape.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;
                double imgWidth = pageWidth;
                double imgHeight = pageHeight;

                long imageShapeId;
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape with a stream creates a foreign (image) shape.
                    imageShapeId = page.AddShape(pinX, pinY, imgWidth, imgHeight, imgStream);
                }

                // Retrieve the image shape object.
                Shape imageShape = page.Shapes.GetShape(imageShapeId);

                // Send the image to the back so it appears behind other shapes.
                imageShape.SendToBack();

                // Make the background image non‑selectable.
                imageShape.Protection.LockSelect.Value = BOOL.True;

                // Locate the triangle shape on the page.
                Shape triangleShape = null;
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
                    throw new Exception("Triangle shape not found on the page.");
                }

                // Ensure the triangle is in front of the background image.
                triangleShape.BringToFront();

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up.
                diagram.Dispose();

                Console.WriteLine("Image inserted behind the triangle and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }