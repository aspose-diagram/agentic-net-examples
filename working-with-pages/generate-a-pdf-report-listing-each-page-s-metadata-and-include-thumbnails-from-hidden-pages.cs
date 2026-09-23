using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Resolve input Visio file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the Visio file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Resolve output PDF report path (second argument or default)
        string reportPath = args.Length > 1 ? args[1] : "Report.pdf";

        // Create a temporary folder for page thumbnail images
        string thumbFolder = Path.Combine(Path.GetTempPath(), "DiagramThumbnails");
        Directory.CreateDirectory(thumbFolder);

        try
        {
            // Load the Visio diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Instantiate a new PDF document (fully qualified to avoid namespace clash)
                var pdfDoc = new Aspose.Pdf.Document();

                // Iterate over each page in the Visio diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the current page (typed as Aspose.Diagram.Page)
                    Page page = diagram.Pages[i];

                    // Add a corresponding page to the PDF document
                    var pdfPage = pdfDoc.Pages.Add();

                    // Determine if the page is hidden by checking UIVisibility enum value
                    bool isHidden = page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Visible ? false : true;

                    // Build a metadata string for the current page
                    string metadata = $"Page ID: {page.ID}, Name: {page.Name}, Universal Name: {page.NameU}, " +
                                      $"Width: {page.PageSheet.PageProps.PageWidth.Value} in, " +
                                      $"Height: {page.PageSheet.PageProps.PageHeight.Value} in, " +
                                      $"Hidden: {isHidden}";

                    // Insert the metadata text into the PDF page
                    var textFragment = new Aspose.Pdf.Text.TextFragment(metadata);
                    pdfPage.Paragraphs.Add(textFragment);

                    // If the page is hidden, generate a PNG thumbnail and embed it
                    if (isHidden)
                    {
                        string thumbPath = Path.Combine(thumbFolder, $"Page_{page.ID}.png");

                        try
                        {
                            // Configure image export options to include hidden pages
                            var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                            {
                                PageIndex = i,          // zero‑based page index
                                PageCount = 1,
                                ExportHiddenPage = true // ensure hidden pages are rendered
                            };
                            // Export the hidden page as a PNG image
                            diagram.Save(thumbPath, imgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export thumbnail for page {page.ID}: {ex.Message}");
                            continue; // Skip embedding if export fails
                        }

                        // Embed the generated thumbnail image into the PDF page
                        try
                        {
                            using (FileStream imgStream = new FileStream(thumbPath, FileMode.Open, FileAccess.Read))
                            {
                                var image = new Aspose.Pdf.Image
                                {
                                    ImageStream = imgStream
                                };
                                pdfPage.Paragraphs.Add(image);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to embed thumbnail for page {page.ID}: {ex.Message}");
                        }
                    }
                }

                // Save the assembled PDF report
                try
                {
                    pdfDoc.Save(reportPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save PDF report: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during diagram processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Attempt to clean up the temporary thumbnail folder
        try
        {
            Directory.Delete(thumbFolder, true);
        }
        catch
        {
            // Ignored: cleanup failure does not affect the generated report
        }

        Console.WriteLine($"PDF report generated at: {Path.GetFullPath(reportPath)}");
    }
}