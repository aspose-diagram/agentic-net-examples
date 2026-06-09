using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramImageConverter <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        byte[] imageData = shape.ForeignData.Value;

                        // Simple JPEG detection via file signature (0xFF,0xD8)
                        if (imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8)
                        {
                            // Convert JPEG to PNG
                            using (MemoryStream inputStream = new MemoryStream(imageData))
                            using (Aspose.Drawing.Image jpegImage = Aspose.Drawing.Image.FromStream(inputStream))
                            using (MemoryStream pngStream = new MemoryStream())
                            {
                                jpegImage.Save(pngStream, ImageFormat.Png);
                                byte[] pngData = pngStream.ToArray();

                                // Replace the foreign data with PNG bytes
                                shape.ForeignData.Value = pngData;

                                Console.WriteLine($"Replaced JPEG image in shape ID {shape.ID} on page '{page.Name}'.");
                            }
                        }
                    }
                }
            }

            // Save the modified diagram (preserving original format)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}