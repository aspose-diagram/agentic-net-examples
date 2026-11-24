using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "VisioReport.pdf";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new PDF document (Aspose.Pdf types are fully qualified)
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page (explicitly typed)
                    Page page = diagram.Pages[i];

                    // Determine hidden status (UIVisibility not equal to Visible)
                    bool isHidden = page.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Visible;

                    // Gather page metadata
                    string metadata = $"Page {i + 1}/{diagram.Pages.Count}\n" +
                                      $"Name: {page.Name}\n" +
                                      $"ID: {page.ID}\n" +
                                      $"Width (in): {page.PageSheet.PageProps.PageWidth.Value}\n" +
                                      $"Height (in): {page.PageSheet.PageProps.PageHeight.Value}\n" +
                                      $"Hidden: {isHidden}\n";

                    // Add a new page to the PDF for this diagram page
                    Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                    // Add metadata text to the PDF page
                    Aspose.Pdf.Text.TextFragment metaFragment = new Aspose.Pdf.Text.TextFragment(metadata);
                    // Position near top-left corner (10 points margin)
                    metaFragment.Position = new Aspose.Pdf.Text.Position(10, pdfPage.PageInfo.Height - 10);
                    pdfPage.Paragraphs.Add(metaFragment);

                    // Generate a thumbnail image (PNG) for the current page, including hidden pages
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imgOptions.PageIndex = i;          // Export only this page
                        imgOptions.PageCount = 1;
                        imgOptions.ExportHiddenPage = true; // Ensure hidden pages are rendered

                        // Save the page as an image to the memory stream
                        diagram.Save(imageStream, imgOptions);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Add the thumbnail image to the PDF page
                        Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                        pdfImage.ImageStream = imageStream;
                        pdfImage.FixWidth = 200; // Scale thumbnail width
                        pdfPage.Paragraphs.Add(pdfImage);
                    }
                }

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the assembled PDF report
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF report generated successfully at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}