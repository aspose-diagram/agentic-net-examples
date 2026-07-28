using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the VDX diagram file
        string diagramPath = "input.vdx";

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram using the VDX format
            Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vdx);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image (foreign) shapes by their type
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Retrieve shape dimensions on the page (in inches)
                        double widthInches = shape.XForm.Width.Value;
                        double heightInches = shape.XForm.Height.Value;
                        Console.WriteLine($"Shape ID {shape.ID}: Page dimensions = {widthInches:F2}\" x {heightInches:F2}\"");

                        // Check for embedded image data within the foreign shape
                        if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                        {
                            // Load the raw image bytes into a memory stream
                            using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                            {
                                // Use the fully qualified Aspose.Drawing.Image to avoid ambiguity
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                {
                                    // Extract pixel dimensions
                                    int pixelWidth = img.Width;
                                    int pixelHeight = img.Height;

                                    // Determine color depth (bits per pixel)
                                    int bitsPerPixel = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat);

                                    Console.WriteLine($"    Pixel dimensions = {pixelWidth} x {pixelHeight}");
                                    Console.WriteLine($"    Color depth = {bitsPerPixel} bits per pixel");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("    No embedded image data found.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}