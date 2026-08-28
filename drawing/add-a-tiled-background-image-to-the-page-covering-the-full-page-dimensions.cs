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
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center point of the page (pin coordinates)
                double centerX = pageWidth / 2.0;
                double centerY = pageHeight / 2.0;

                // Path to the background image file (must exist on disk)
                const string imagePath = "background.png";

                // Insert the image as a shape that spans the entire page
                long bgShapeId;
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape(pinX, pinY, width, height, Stream) returns the shape ID
                    bgShapeId = page.AddShape(centerX, centerY, pageWidth, pageHeight, imgStream);
                }

                // Retrieve the shape object using the returned ID
                Shape bgShape = page.Shapes.GetShape(bgShapeId);

                // Set the fill pattern to picture (value 25) to enable tiling
                bgShape.Fill.FillPattern.Value = 25;

                // Send the background shape to the back so other shapes appear above it
                bgShape.SendToBack();

                // Make the background non‑selectable
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
