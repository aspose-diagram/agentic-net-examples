using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is output PDF path, remaining are Visio file paths
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <output.pdf> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPdfPath = args[0];
        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a temporary folder to store shape images
        string tempImageFolder = Path.Combine(Path.GetTempPath(), "VisioShapeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempImageFolder);

        // List to keep track of generated image file paths
        List<string> imageFiles = new List<string>();

        // Process each Visio file
        for (int i = 1; i < args.Length; i++)
        {
            string visioPath = args[i];

            // Guard: check file existence
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"Input file does not exist: {visioPath}");
                continue;
            }

            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Iterate through all pages
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Aspose.Diagram.Shape shape in page.Shapes)
                        {
                            // Generate a unique file name for each shape image
                            string shapeImagePath = Path.Combine(
                                tempImageFolder,
                                $"img_{Path.GetFileNameWithoutExtension(visioPath)}_p{page.ID}_s{shape.ID}.png");

                            // Export the shape to PNG using ImageSaveOptions
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            shape.ToImage(shapeImagePath, imgOptions);

                            // Record the image path for later PDF assembly
                            imageFiles.Add(shapeImagePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{visioPath}': {ex.Message}");
            }
        }

        // If no images were generated, abort PDF creation
        if (imageFiles.Count == 0)
        {
            Console.Error.WriteLine("No shape images were generated. PDF will not be created.");
            // Cleanup temporary folder
            Directory.Delete(tempImageFolder, true);
            return;
        }

        try
        {
            // Create a new PDF document using fully qualified Aspose.Pdf types
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Add each image as a separate page in the PDF
            foreach (string imgPath in imageFiles)
            {
                // Ensure the image file still exists
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image file missing: {imgPath}");
                    continue;
                }

                // Add a new page to the PDF
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                // Create an image object and assign the image stream
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();

                using (FileStream imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    pdfImage.ImageStream = imgStream;
                    // Add the image to the page's paragraphs collection
                    pdfPage.Paragraphs.Add(pdfImage);
                }
            }

            // Save the assembled PDF to the specified output path
            pdfDoc.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating PDF '{outputPdfPath}': {ex.Message}");
        }
        finally
        {
            // Cleanup temporary images folder
            try
            {
                if (Directory.Exists(tempImageFolder))
                {
                    Directory.Delete(tempImageFolder, true);
                }
            }
            catch (Exception cleanupEx)
            {
                Console.Error.WriteLine($"Error cleaning up temporary files: {cleanupEx.Message}");
            }
        }
    }
}