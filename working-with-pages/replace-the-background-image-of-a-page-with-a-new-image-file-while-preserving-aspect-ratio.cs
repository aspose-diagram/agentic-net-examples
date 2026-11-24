using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <imagePath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        string imagePath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Work with the first page (index 0). Adjust as needed.
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Load the new background image to obtain its pixel dimensions.
                using (FileStream imgFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(imgFileStream))
                {
                    // Image dimensions in pixels.
                    double imgWidthPx = img.Width;
                    double imgHeightPx = img.Height;

                    // Convert pixel dimensions to inches assuming 96 DPI (default for Aspose.Drawing).
                    // This conversion keeps the aspect ratio while fitting the page.
                    const double dpi = 96.0;
                    double imgWidthIn = imgWidthPx / dpi;
                    double imgHeightIn = imgHeightPx / dpi;

                    // Compute scaling factor to fit the image within the page while preserving aspect ratio.
                    double scale = Math.Min(pageWidth / imgWidthIn, pageHeight / imgHeightIn);

                    // Calculate the final size of the image on the page.
                    double finalWidth = imgWidthIn * scale;
                    double finalHeight = imgHeightIn * scale;

                    // Center the image on the page.
                    double pinX = (pageWidth - finalWidth) / 2.0;
                    double pinY = (pageHeight - finalHeight) / 2.0;

                    // Re‑open the image stream for AddShape (the previous stream was consumed by Image.FromStream).
                    imgFileStream.Position = 0;

                    // Insert the image as a shape on the page.
                    long shapeId = page.AddShape(pinX, pinY, finalWidth, finalHeight, imgFileStream);

                    // Retrieve the shape object to adjust its Z‑order.
                    Shape bgShape = page.Shapes.GetShape(shapeId);
                    bgShape.SendToBack(); // Ensure the background image is behind other content.
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Background image replaced successfully. Saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}