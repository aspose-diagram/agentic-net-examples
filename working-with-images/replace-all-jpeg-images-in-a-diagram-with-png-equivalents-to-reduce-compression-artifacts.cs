using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Output Visio file path
        string outputPath = "output_converted.vsdx";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is a foreign (image) shape with embedded data
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] imageData = shape.ForeignData.Value;

                        // Load the image from the byte array using Aspose.Drawing.Image
                        using (MemoryStream ms = new MemoryStream(imageData))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Determine if the image is JPEG
                            if (img.RawFormat.Equals(ImageFormat.Jpeg))
                            {
                                // Convert JPEG to PNG
                                using (MemoryStream pngMs = new MemoryStream())
                                {
                                    img.Save(pngMs, ImageFormat.Png);
                                    byte[] pngData = pngMs.ToArray();

                                    // Replace the foreign data with PNG bytes
                                    shape.ForeignData.Value = pngData;

                                    Console.WriteLine($"Replaced JPEG image in shape ID {shape.ID} on page '{page.Name}'.");
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram (preserving VSDX format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram processing completed.");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}