using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input Visio file, background image file, output PDF file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <backgroundImagePath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string imagePath = args[1];
        string outputPdfPath = args[2];

        // Guard against missing input files
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image file not found: {imagePath}");
            return;
        }

        // Read the background image into a byte array for reuse
        byte[] imageBytes;
        try
        {
            imageBytes = File.ReadAllBytes(imagePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read background image: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over all pages and add the background image shape
                foreach (Page page in diagram.Pages)
                {
                    // Create a fresh memory stream for each page
                    using (MemoryStream imgStream = new MemoryStream(imageBytes))
                    {
                        // Add the image as a shape; position (0,0) and size (0,0) let Visio use the image's native dimensions
                        long shapeId = page.AddShape(0, 0, 0, 0, imgStream);

                        // Retrieve the shape object to adjust its Z-order
                        Shape bgShape = page.Shapes.GetShape(shapeId);
                        if (bgShape != null)
                        {
                            // Send the image shape to the back so it appears as a background
                            bgShape.SendToBack();
                        }
                    }
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format (optional but clarifies intent)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export the updated diagram to PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"Diagram saved to PDF: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }
    }
}