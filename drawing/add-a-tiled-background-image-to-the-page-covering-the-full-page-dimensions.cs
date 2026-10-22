using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;
using AsposeDrawing = Aspose.Drawing;

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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Work with the first page (index 0).
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches).
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Determine the image size in inches using its resolution.
            double tileWidth;
            double tileHeight;
            using (FileStream imgInfoStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (AsposeDrawing.Image img = AsposeDrawing.Image.FromStream(imgInfoStream))
            {
                // Width and Height are in pixels; divide by DPI to get inches.
                tileWidth = img.Width / img.HorizontalResolution;
                tileHeight = img.Height / img.VerticalResolution;
            }

            // If the image dimensions could not be determined, fall back to full‑page size.
            if (tileWidth <= 0 || tileHeight <= 0)
            {
                tileWidth = pageWidth;
                tileHeight = pageHeight;
            }

            // Tile the image across the entire page.
            for (double y = 0; y < pageHeight; y += tileHeight)
            {
                for (double x = 0; x < pageWidth; x += tileWidth)
                {
                    // Ensure the tile does not exceed page bounds.
                    double currentWidth = Math.Min(tileWidth, pageWidth - x);
                    double currentHeight = Math.Min(tileHeight, pageHeight - y);

                    // Add the image shape.
                    using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        long shapeId = page.AddShape(x, y, currentWidth, currentHeight, imgStream);
                        Shape shape = page.Shapes.GetShape(shapeId);
                        // Send the shape to the back so it acts as a background.
                        shape.SendToBack();
                        // Make the background shape non‑selectable.
                        shape.Protection.LockSelect.Value = BOOL.True;
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with tiled background to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}