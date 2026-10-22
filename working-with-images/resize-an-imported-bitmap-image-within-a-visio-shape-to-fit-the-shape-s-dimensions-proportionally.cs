using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Visio uses 96 DPI by default – used for inch‑to‑pixel conversion.
            const double dpi = 96.0;

            // Iterate over all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Process only imported bitmap images (Foreign type).
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                    {
                        byte[] originalBytes = shape.ForeignData.Value;

                        // Load the original image using Aspose.Drawing.
                        using (MemoryStream originalStream = new MemoryStream(originalBytes))
                        using (Aspose.Drawing.Image originalImage = Aspose.Drawing.Image.FromStream(originalStream))
                        {
                            // Original pixel dimensions.
                            int origPixelWidth = originalImage.Width;
                            int origPixelHeight = originalImage.Height;

                            // Shape dimensions in inches.
                            double shapeWidthInches = shape.XForm.Width.Value;
                            double shapeHeightInches = shape.XForm.Height.Value;

                            // Target pixel dimensions based on shape size.
                            int targetPixelWidth = (int)Math.Round(shapeWidthInches * dpi);
                            int targetPixelHeight = (int)Math.Round(shapeHeightInches * dpi);

                            // Compute scaling factor while preserving aspect ratio.
                            double scaleX = (double)targetPixelWidth / origPixelWidth;
                            double scaleY = (double)targetPixelHeight / origPixelHeight;
                            double scale = Math.Min(scaleX, scaleY);

                            // New pixel dimensions after scaling.
                            int newPixelWidth = (int)Math.Round(origPixelWidth * scale);
                            int newPixelHeight = (int)Math.Round(origPixelHeight * scale);

                            // Resize the image.
                            using (Bitmap resizedBitmap = new Bitmap(newPixelWidth, newPixelHeight))
                            using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                            {
                                // High‑quality resizing settings.
                                graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(originalImage, 0, 0, newPixelWidth, newPixelHeight);

                                // Save the resized image to a memory stream using the original format.
                                using (MemoryStream resizedStream = new MemoryStream())
                                {
                                    ImageFormat format = originalImage.RawFormat;
                                    resizedBitmap.Save(resizedStream, format);
                                    byte[] resizedBytes = resizedStream.ToArray();

                                    // Replace the shape's foreign data with the resized image bytes.
                                    shape.ForeignData.Value = resizedBytes;

                                    // Adjust the shape's size to match the resized image (optional).
                                    shape.XForm.Width.Value = newPixelWidth / dpi;
                                    shape.XForm.Height.Value = newPixelHeight / dpi;
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
