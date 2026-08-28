using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the VDX file (modify as needed)
            string diagramPath = "input.vdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vdx);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image (foreign) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Retrieve shape dimensions (in inches)
                        double widthInches = shape.XForm.Width.Value;
                        double heightInches = shape.XForm.Height.Value;

                        Console.WriteLine($"Shape ID: {shape.ID}");
                        Console.WriteLine($"  Dimensions: {widthInches:F2}\" x {heightInches:F2}\"");

                        // Retrieve raw image data
                        byte[] imageData = shape.ForeignData.Value;
                        if (imageData == null || imageData.Length == 0)
                        {
                            Console.WriteLine("  No image data found.");
                            continue;
                        }

                        // Load image using Aspose.Drawing to get color depth
                        using (MemoryStream ms = new MemoryStream(imageData))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            int colorDepth = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat);
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
