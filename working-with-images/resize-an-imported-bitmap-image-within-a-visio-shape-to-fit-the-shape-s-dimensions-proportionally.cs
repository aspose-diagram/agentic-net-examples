using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Drawing.Drawing2D;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                const double dpi = 96.0; // DPI for conversion from inches to pixels

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only foreign (bitmap) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            // Retrieve shape dimensions in inches
                            double shapeWidthInches = shape.XForm.Width.Value;
                            double shapeHeightInches = shape.XForm.Height.Value;

                            // Convert shape dimensions to target pixel size
                            int targetWidthPx = (int)Math.Round(shapeWidthInches * dpi);
                            int targetHeightPx = (int)Math.Round(shapeHeightInches * dpi);

                            // Load the embedded bitmap image from the foreign data byte array
                            using (MemoryStream srcStream = new MemoryStream(shape.ForeignData.Value))
                            {
                                // Fully qualify Aspose.Drawing.Image to avoid ambiguity
                                using (Aspose.Drawing.Image srcImage = Aspose.Drawing.Image.FromStream(srcStream))
                                {
                                    int originalWidth = srcImage.Width;
                                    int originalHeight = srcImage.Height;

                                    // Calculate scaling factor to fit proportionally
                                    double scale = Math.Min((double)targetWidthPx / originalWidth,
                                                            (double)targetHeightPx / originalHeight);

                                    // Optional: prevent upscaling (comment out if upscaling is desired)
                                    // if (scale > 1) scale = 1;

                                    int newWidth = (int)Math.Round(originalWidth * scale);
                                    int newHeight = (int)Math.Round(originalHeight * scale);

                                    // Create a resized bitmap
                                    using (Bitmap resizedBitmap = new Bitmap(newWidth, newHeight))
                                    {
                                        using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                                        {
                                            // Use high quality interpolation for better results
                                            graphics.InterpolationMode = Aspose.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; // Fixed ambiguous reference
                                            graphics.DrawImage(srcImage, 0, 0, newWidth, newHeight);
                                        }

                                        // Preserve original image format
                                        ImageFormat format = srcImage.RawFormat;

                                        // Save resized image to a memory stream
                                        using (MemoryStream dstStream = new MemoryStream())
                                        {
                                            resizedBitmap.Save(dstStream, format);
                                            shape.ForeignData.Value = dstStream.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the output path with explicit format argument
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
