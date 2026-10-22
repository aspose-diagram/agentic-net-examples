using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output combined PDF path
        string outputPdfPath = "CombinedShapes.pdf";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // List to hold temporary EMF file paths
        List<string> tempEmfFiles = new List<string>();

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Create a temporary EMF file name for the current page
                    string tempEmfPath = Path.Combine(Path.GetTempPath(), $"page_{i + 1}_{Guid.NewGuid()}.emf");
                    tempEmfFiles.Add(tempEmfPath);

                    // Save the current page as EMF (vector format)
                    // Use ImageSaveOptions with the specific page index
                    ImageSaveOptions emfOptions = new ImageSaveOptions(SaveFileFormat.Emf)
                    {
                        PageIndex = i,
                        PageCount = 1
                    };
                    diagram.Save(tempEmfPath, emfOptions);
                }
            }

            // Create a new PDF document (fully qualified to avoid namespace ambiguity)
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

            // Add each EMF as a separate page in the PDF
            foreach (string emfPath in tempEmfFiles)
            {
                // Add a new page to the PDF
                Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                // Create an image object from the EMF file
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                pdfImage.ImageStream = new FileStream(emfPath, FileMode.Open, FileAccess.Read);
                // Fit the image to the page dimensions
                pdfImage.FixWidth = pdfPage.PageInfo.Width;
                pdfImage.FixHeight = pdfPage.PageInfo.Height;

                // Add the image to the page's paragraphs collection
                pdfPage.Paragraphs.Add(pdfImage);
            }

            // Save the combined multi‑page PDF document
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary EMF files
            foreach (string tempFile in tempEmfFiles)
            {
                try
                {
                    if (File.Exists(tempFile))
                        File.Delete(tempFile);
                }
                catch
                {
                    // Suppress any cleanup errors
                }
            }
        }
    }
}