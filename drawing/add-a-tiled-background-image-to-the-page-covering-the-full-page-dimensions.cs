using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (contains a default page)
            Diagram diagram = new Diagram();

            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Optionally set page size (e.g., A4)
            page.PageSheet.PageProps.PageWidth.Value = 8.27;   // inches
            page.PageSheet.PageProps.PageHeight.Value = 11.69; // inches

            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Path to the image that will be tiled as background
            string imagePath = "background.png";

            // Add a shape that covers the entire page using the image stream
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                // Center of the page
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // AddShape returns the shape ID (long)
                long shapeId = page.AddShape(pinX, pinY, pageWidth, pageHeight, fs);

                // Retrieve the shape object
                Shape bgShape = page.Shapes.GetShape((int)shapeId);

                // Set fill pattern to a texture (tile) – pattern 25 is the texture pattern
                bgShape.Fill.FillPattern.Value = 25;

                // Optional: set a background color for the texture
                bgShape.Fill.FillBkgnd.Value = "#FFFFFF";

                // Send the background shape to the back so other shapes appear above it
                bgShape.SendToBack();

                // Make the background non‑selectable
                bgShape.Protection.LockSelect.Value = BOOL.True;
            }

            // Save the diagram in Visio format
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
