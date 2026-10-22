using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputHtmlPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to locate embedded images.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only foreign (image) shapes that contain data.
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData?.Value != null)
                    {
                        byte[] originalData = shape.ForeignData.Value;

                        // Re‑encode the image to JPEG to achieve compression.
                        using (MemoryStream sourceStream = new MemoryStream(originalData))
                        using (Aspose.Drawing.Image image = Aspose.Drawing.Image.FromStream(sourceStream))
                        using (MemoryStream jpegStream = new MemoryStream())
                        {
                            // Save the image as JPEG using the default quality.
                            image.Save(jpegStream, Aspose.Drawing.Imaging.ImageFormat.Jpeg);
                            // Replace the shape's foreign data with the compressed JPEG bytes.
                            shape.ForeignData.Value = jpegStream.ToArray();
                        }
                    }
                }
            }

            // Prepare HTML export options.
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Export the diagram to HTML.
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}