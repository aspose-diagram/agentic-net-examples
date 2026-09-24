using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Folder containing Visio files (change as needed)
        string inputFolder = @"C:\VisioFiles";
        // Verify the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Output PDF file
        string outputPdfPath = @"C:\Output\CombinedShapes.pdf";

        // Collect temporary image file paths
        List<string> imageFiles = new List<string>();

        // Process each Visio file in the folder
        foreach (string visioPath in Directory.GetFiles(inputFolder, "*.vsdx"))
        {
            // Verify the Visio file exists (should always be true from GetFiles, but guard per rules)
            if (!File.Exists(visioPath))
            {
                Console.Error.WriteLine($"File not found: {visioPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Export the shape to a PNG image
                        string tempImagePath = Path.Combine(Path.GetTempPath(),
                            Guid.NewGuid().ToString() + ".png");

                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(tempImagePath, imgOptions);
                        imageFiles.Add(tempImagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing a Visio file
                Console.Error.WriteLine($"Error processing '{visioPath}': {ex.Message}");
            }
        }

        try
        {
            // Create a new PDF document using fully qualified Aspose.Pdf types
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Add each image as a separate page
            foreach (string imgPath in imageFiles)
            {
                // Add a new page
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                // Create an image object and set its source file
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                pdfImage.File = imgPath;

                // Fit the image to the page dimensions
                pdfImage.FixWidth = pdfPage.PageInfo.Width;
                pdfImage.FixHeight = pdfPage.PageInfo.Height;

                // Add the image to the page
                pdfPage.Paragraphs.Add(pdfImage);
            }

            // Save the combined PDF
            pdfDoc.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during PDF creation
            Console.Error.WriteLine($"Error creating PDF: {ex.Message}");
            return;
        }

        // Clean up temporary image files
        foreach (string imgPath in imageFiles)
        {
            try
            {
                File.Delete(imgPath);
            }
            catch
            {
                // Ignore any errors during cleanup
            }
        }

        Console.WriteLine($"PDF created successfully at: {outputPdfPath}");
    }
}