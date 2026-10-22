using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only picture (foreign) shapes that contain image data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                        {
                            // Load the image bytes into an Aspose.Drawing.Image
                            using (MemoryStream inputStream = new MemoryStream(shape.ForeignData.Value))
                            {
                                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(inputStream))
                                {
                                    // Ensure we are working with a Bitmap for pixel manipulation
                                    using (Aspose.Drawing.Bitmap bmp = new Aspose.Drawing.Bitmap(img))
                                    {
                                        // Apply grayscale conversion pixel by pixel
                                        for (int y = 0; y < bmp.Height; y++)
                                        {
                                            for (int x = 0; x < bmp.Width; x++)
                                            {
                                                Aspose.Drawing.Color original = bmp.GetPixel(x, y);
                                                // Compute luminance using standard coefficients
                                                int gray = (int)(0.299 * original.R + 0.587 * original.G + 0.114 * original.B);
                                                gray = Math.Clamp(gray, 0, 255);
                                                Aspose.Drawing.Color grayColor = Aspose.Drawing.Color.FromArgb(original.A, gray, gray, gray);
                                                bmp.SetPixel(x, y, grayColor);
                                            }
                                        }

                                        // Save the modified bitmap back to a byte array (PNG format)
                                        using (MemoryStream outputStream = new MemoryStream())
                                        {
                                            bmp.Save(outputStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                                            shape.ForeignData.Value = outputStream.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set the save format explicitly
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: do not export hidden pages (bool, not BOOL)
                pdfOptions.ExportHiddenPage = false; // corrected from BOOL.False

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
