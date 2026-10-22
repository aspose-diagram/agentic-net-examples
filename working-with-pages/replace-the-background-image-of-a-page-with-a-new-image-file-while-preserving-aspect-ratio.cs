using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // New background image file path
        string imagePath = "background.png";
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Load image bytes
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Obtain image dimensions using Aspose.Drawing.Image (fully qualified to avoid ambiguity)
                double imgWidth;
                double imgHeight;
                using (var ms = new MemoryStream(imageBytes))
                using (var img = Aspose.Drawing.Image.FromStream(ms))
                {
                    imgWidth = img.Width;
                    imgHeight = img.Height;
                }

                // Compute scaling factor to fit image within page while preserving aspect ratio
                double scale = Math.Min(pageWidth / imgWidth, pageHeight / imgHeight);
                double newWidth = imgWidth * scale;
                double newHeight = imgHeight * scale;

                // Compute lower‑left corner (pinX, pinY) to center the image on the page
                double pinX = (pageWidth - newWidth) / 2.0;
                double pinY = (pageHeight - newHeight) / 2.0;

                // Try to find an existing background image shape (foreign type)
                Shape backgroundShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign)
                    {
                        backgroundShape = shape;
                        break;
                    }
                }

                if (backgroundShape != null)
                {
                    // Replace the existing shape's image data
                    backgroundShape.ForeignData.Value = imageBytes;

                    // Update shape size and position (PinX/Y are shape centers)
                    backgroundShape.XForm.Width.Value = newWidth;
                    backgroundShape.XForm.Height.Value = newHeight;
                    backgroundShape.XForm.PinX.Value = pinX + newWidth / 2.0;
                    backgroundShape.XForm.PinY.Value = pinY + newHeight / 2.0;

                    // Send the shape to back and lock selection
                    backgroundShape.SendToBack();
                    backgroundShape.Protection.LockSelect.Value = BOOL.True;
                }
                else
                {
                    // No existing background shape; add a new one using the image stream
                    using (var imgStream = new MemoryStream(imageBytes))
                    {
                        long shapeId = page.AddShape(pinX, pinY, newWidth, newHeight, imgStream);
                        Shape newShape = page.Shapes.GetShape(shapeId);
                        newShape.SendToBack();
                        newShape.Protection.LockSelect.Value = BOOL.True;
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}