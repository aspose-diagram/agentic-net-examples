using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: diagram file, new background image, output diagram file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <imagePath> <outputPath>");
            return;
        }

        string diagramPath = args[0];
        // Guard: ensure diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        string imagePath = args[1];
        // Guard: ensure image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"File not found: {imagePath}");
            return;
        }

        string outputPath = args[2];

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Use the first page (index 0) as the target page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Load the image to obtain its pixel size and DPI using Aspose.Drawing.Image
            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(imagePath))
            {
                // Convert pixel dimensions to inches using DPI
                double imgWidthInches = img.Width / img.HorizontalResolution;
                double imgHeightInches = img.Height / img.VerticalResolution;

                // Compute scaling factor to fit the image within the page while preserving aspect ratio
                double scale = Math.Min(pageWidth / imgWidthInches, pageHeight / imgHeightInches);

                // Calculate the final width and height for the shape (still in inches)
                double targetWidth = imgWidthInches * scale;
                double targetHeight = imgHeightInches * scale;

                // Center the shape on the page (PinX/PinY represent the shape's center)
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the image as a shape on the page
                using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // AddShape returns the shape ID (long)
                    long shapeId = page.AddShape(pinX, pinY, targetWidth, targetHeight, imgStream);

                    // Retrieve the shape object to adjust its properties
                    Shape bgShape = page.Shapes.GetShape(shapeId);

                    // Send the image shape to the back so it appears behind other content
                    bgShape.SendToBack();

                    // Make the background non‑selectable to avoid accidental edits
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Save the modified diagram (preserve original format if possible)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Background image replaced successfully. Saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}