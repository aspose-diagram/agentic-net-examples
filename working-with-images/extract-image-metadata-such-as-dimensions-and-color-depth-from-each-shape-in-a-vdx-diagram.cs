using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input_vdx_path>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram using the mandatory using pattern
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes by checking for foreign type
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Extract shape dimensions in inches
                            double widthInches = shape.XForm.Width.Value;
                            double heightInches = shape.XForm.Height.Value;

                            // Extract image metadata from the embedded binary data
                            if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                                {
                                    // Load the image using Aspose.Drawing
                                    using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                    {
                                        int pixelWidth = img.Width;
                                        int pixelHeight = img.Height;
                                        int colorDepth = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat);

                                        // Output the collected metadata
                                        Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                                        Console.WriteLine($"  Dimensions (inches): {widthInches:F2} x {heightInches:F2}");
                                        Console.WriteLine($"  Pixel Size: {pixelWidth} x {pixelHeight}");
                                        Console.WriteLine($"  Color Depth: {colorDepth} bits per pixel");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID} - No image data found.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}