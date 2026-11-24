using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input arguments with defaults
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string imagePath = args.Length > 1 ? args[1] : "background.png";
        string outputPath = args.Length > 2 ? args[2] : "output.vsdx";

        // Guard against missing files
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
            // Load image to obtain its dimensions (pixels)
            double tileWidth;
            double tileHeight;
            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(imagePath))
            {
                tileWidth = img.Width;
                tileHeight = img.Height;
            }

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Work with the first (foreground) page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Tile the image across the entire page
                for (double y = 0; y < pageHeight; y += tileHeight)
                {
                    for (double x = 0; x < pageWidth; x += tileWidth)
                    {
                        // Insert the image as a shape at the current tile position
                        using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            long shapeId = page.AddShape(x, y, tileWidth, tileHeight, fs);
                            Shape shape = page.Shapes.GetShape(shapeId);
                            // Send the shape to the back so other content appears above it
                            shape.SendToBack();
                            // Make the background shape non‑selectable
                            shape.Protection.LockSelect.Value = BOOL.True;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}