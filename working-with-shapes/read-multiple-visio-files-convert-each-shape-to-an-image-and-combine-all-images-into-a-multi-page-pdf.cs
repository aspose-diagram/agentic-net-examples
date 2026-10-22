using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: at least one input file and one output PDF path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program.exe <output.pdf> <input1.vsdx> [<input2.vsdx> ...]");
            return;
        }

        string outputPdfPath = args[0];
        string[] inputFiles = new string[args.Length - 1];
        Array.Copy(args, 1, inputFiles, 0, inputFiles.Length);

        // Guard for output directory existence
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // Verify each input file exists
        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Input file not found: {filePath}");
                return;
            }
        }

        // Create a temporary folder for shape images
        string tempImageFolder = Path.Combine(Path.GetTempPath(), "VisioShapeImages_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempImageFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary folder: {ex.Message}");
            return;
        }

        // List to hold all generated image file paths
        List<string> allImagePaths = new List<string>();

        // Process each Visio file
        foreach (string visioPath in inputFiles)
        {
            try
            {
                // Load diagram inside a using block as required
                using (Diagram diagram = new Diagram(visioPath))
                {
                    int pageIndex = 0;
                    // Iterate through pages
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;
                        // Iterate through shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Build a unique image file name for each shape
                            string imageFileName = $"{Path.GetFileNameWithoutExtension(visioPath)}_Page{pageIndex}_Shape{shape.ID}.png";
                            string imagePath = Path.Combine(tempImageFolder, imageFileName);

                            try
                            {
                                // Export shape to PNG image
                                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                                shape.ToImage(imagePath, imgOptions);
                                allImagePaths.Add(imagePath);
                            }
                            catch (Exception imgEx)
                            {
                                Console.Error.WriteLine($"Failed to export shape ID {shape.ID} from '{visioPath}' (Page {pageIndex}): {imgEx.Message}");
                                // Continue with next shape
                            }
                        }
                    }
                }
            }
            catch (Exception diagEx)
            {
                Console.Error.WriteLine($"Failed to process Visio file '{visioPath}': {diagEx.Message}");
                // Continue with next file
            }
        }

        // Create PDF document and add each image as a separate page
        try
        {
            // Fully qualified Aspose.Pdf types are used to avoid namespace conflicts
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            foreach (string imgPath in allImagePaths)
            {
                // Load image to obtain its dimensions
                using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromFile(imgPath))
                {
                    // Add a new page to the PDF
                    Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                    // Set page size to match image dimensions (points: 1 point = 1/72 inch, but using pixel size directly)
                    pdfPage.PageInfo.Width = img.Width;
                    pdfPage.PageInfo.Height = img.Height;

                    // Create PDF image object and attach the file
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.File = imgPath;

                    // Add the image to the page's paragraphs collection
                    pdfPage.Paragraphs.Add(pdfImage);
                }
            }

            // Save the combined PDF
            pdfDoc.Save(outputPdfPath);
        }
        catch (Exception pdfEx)
        {
            Console.Error.WriteLine($"Failed to create PDF '{outputPdfPath}': {pdfEx.Message}");
            // Cleanup temporary images before exiting
            CleanupTempFolder(tempImageFolder);
            return;
        }

        // Cleanup temporary images
        CleanupTempFolder(tempImageFolder);
    }

    // Helper method to delete temporary folder and its contents
    private static void CleanupTempFolder(string folderPath)
    {
        try
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
        catch (Exception cleanupEx)
        {
            Console.Error.WriteLine($"Failed to clean up temporary folder '{folderPath}': {cleanupEx.Message}");
        }
    }
}