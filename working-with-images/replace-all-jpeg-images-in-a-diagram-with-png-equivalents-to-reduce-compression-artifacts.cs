using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify image shapes (foreign objects)
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Ensure the shape contains foreign data (image bytes)
                            if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                            {
                                byte[] imageData = shape.ForeignData.Value;

                                // Detect JPEG format by its magic numbers (0xFF, 0xD8)
                                if (imageData.Length >= 2 && imageData[0] == 0xFF && imageData[1] == 0xD8)
                                {
                                    // Convert JPEG bytes to PNG using Aspose.Drawing
                                    using (MemoryStream inputStream = new MemoryStream(imageData))
                                    {
                                        // Load the JPEG image
                                        using (Aspose.Drawing.Image jpegImage = Aspose.Drawing.Image.FromStream(inputStream))
                                        {
                                            using (MemoryStream pngStream = new MemoryStream())
                                            {
                                                // Save the image as PNG into the memory stream
                                                jpegImage.Save(pngStream, Aspose.Drawing.Imaging.ImageFormat.Png);

                                                // Replace the shape's image data with the new PNG bytes
                                                shape.ForeignData.Value = pngStream.ToArray();

                                                // Ensure the image shape stays behind other content
                                                shape.SendToBack();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}