using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VDX diagram file
            string diagramPath = "input.vdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vdx);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image (foreign) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                    {
                        // Extract the raw image bytes
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Load the image using Aspose.Drawing
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Get image dimensions in pixels
                            int width = img.Width;
                            int height = img.Height;

                            // Get color depth (bits per pixel)
                            int colorDepth = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat);

                            // Output metadata
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}");
                            Console.WriteLine($"  Dimensions: {width} x {height} pixels");
                            Console.WriteLine($"  Color Depth: {colorDepth} bits per pixel");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
