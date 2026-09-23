using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file, background image file and output Visio file paths.
        string diagramPath = "input.vsdx";
        if (!File.Exists(diagramPath)) { Console.Error.WriteLine($"File not found: {diagramPath}"); return; }

        string imagePath = "background.png";
        if (!File.Exists(imagePath)) { Console.Error.WriteLine($"File not found: {imagePath}"); return; }

        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Get the first page (you can change the index or retrieve by name if needed).
            Page page = diagram.Pages[0];

            // Hide any existing background image shapes (type Foreign) on the page.
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Type == TypeValue.Foreign)
                {
                    // Mark shape as deleted (hidden) while preserving it in the document.
                    shape.Del = BOOL.True;
                }
            }

            // Load the image to obtain its pixel dimensions and DPI.
            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(imagePath))
            {
                double imgWidthInches = img.Width / img.HorizontalResolution;
                double imgHeightInches = img.Height / img.VerticalResolution;

                // Page dimensions (in inches).
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Compute scaling factor to preserve aspect ratio and fit within the page.
                double scale = Math.Min(pageWidth / imgWidthInches, pageHeight / imgHeightInches);

                double finalWidth = imgWidthInches * scale;
                double finalHeight = imgHeightInches * scale;

                // Center position for the background shape.
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Insert the image as a shape on the page.
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    long shapeId = page.AddShape(pinX, pinY, finalWidth, finalHeight, fs);
                    Shape bgShape = page.Shapes.GetShape((int)shapeId);

                    // Send the image to the back so other shapes appear above it.
                    bgShape.SendToBack();

                    // Make the background shape non‑selectable.
                    bgShape.Protection.LockSelect.Value = BOOL.True;
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}