using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a VDX file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image (foreign) shapes.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Retrieve the shape's size in inches.
                            double widthInches = shape.XForm.Width.Value;
                            double heightInches = shape.XForm.Height.Value;

                            // Access the raw image bytes stored in the shape.
                            byte[] imageData = shape.ForeignData.Value;

                            if (imageData != null && imageData.Length > 0)
                            {
                                // Load the image from the byte array to extract pixel metadata.
                                using (MemoryStream ms = new MemoryStream(imageData))
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                                {
                                    int pixelWidth = img.Width;
                                    int pixelHeight = img.Height;
                                    int colorDepth = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat); // bits per pixel

                                    // Output the collected metadata.
                                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                                    Console.WriteLine($" - Size (inches): {widthInches:F2} x {heightInches:F2}");
                                    Console.WriteLine($" - Pixel dimensions: {pixelWidth} x {pixelHeight}");
                                    Console.WriteLine($" - Color depth: {colorDepth} bits per pixel");
                                }
                            }
                            else
                            {
                                // Shape reports as an image but contains no data.
                                Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name} contains no image data.");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}