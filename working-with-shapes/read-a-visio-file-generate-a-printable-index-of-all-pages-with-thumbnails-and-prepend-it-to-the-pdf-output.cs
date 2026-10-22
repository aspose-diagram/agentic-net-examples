using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for thumbnails and intermediate PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "VisioIndexThumbnails_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Lists to store thumbnail file paths and page names
                List<string> thumbnailPaths = new List<string>();
                List<string> pageNames = new List<string>();

                // Generate a PNG thumbnail for each page
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    string pageName = page.NameU ?? $"Page {i + 1}";
                    pageNames.Add(pageName);

                    string thumbPath = Path.Combine(tempDir, $"thumb_{i}.png");
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = i;   // Export only the current page
                    imgOptions.PageCount = 1;   // Single page
                    diagram.Save(thumbPath, imgOptions);
                    thumbnailPaths.Add(thumbPath);
                }

                // Export the full diagram to a temporary PDF (without the index page)
                string tempPdfPath = Path.Combine(tempDir, "full.pdf");
                Aspose.Diagram.Saving.PdfSaveOptions pdfOptions = new Aspose.Diagram.Saving.PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.ExportHiddenPage = false;
                diagram.Save(tempPdfPath, pdfOptions);

                // ------------------------------------------------------------
                // Build an index PDF page using Aspose.Pdf (fully qualified types)
                // ------------------------------------------------------------
                Aspose.Pdf.Document indexDoc = new Aspose.Pdf.Document();
                Aspose.Pdf.Page indexPage = indexDoc.Pages.Add();

                // Layout configuration: two columns of thumbnails
                const int columns = 2;
                double margin = 20;
                double thumbWidth = 200;   // Width in points
                double thumbHeight = 150;  // Height in points
                double x = margin;
                double y = margin;

                for (int i = 0; i < thumbnailPaths.Count; i++)
                {
                    // Add the thumbnail image
                    Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                    img.ImageStream = new FileStream(thumbnailPaths[i], FileMode.Open, FileAccess.Read);
                    img.FixWidth = thumbWidth;
                    img.FixHeight = thumbHeight;
                    // Use MarginInfo to position the image (Image does not have a Position property)
                    img.Margin = new Aspose.Pdf.MarginInfo(x, y, 0, 0);
                    indexPage.Paragraphs.Add(img);

                    // Add a caption below the thumbnail
                    Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(pageNames[i]);
                    tf.Position = new Aspose.Pdf.Text.Position(x, y + thumbHeight + 5);
                    tf.TextState.FontSize = 12;
                    indexPage.Paragraphs.Add(tf);

                    // Update coordinates for next thumbnail
                    if ((i + 1) % columns == 0)
                    {
                        x = margin;
                        y += thumbHeight + 30; // Image height + caption + spacing
                    }
                    else
                    {
                        x += thumbWidth + margin;
                    }
                }

                // Load the full PDF generated from the diagram
                Aspose.Pdf.Document fullDoc = new Aspose.Pdf.Document(tempPdfPath);

                // Create the final PDF document and prepend the index page
                Aspose.Pdf.Document finalDoc = indexDoc; // indexDoc already contains the index page
                foreach (Aspose.Pdf.Page p in fullDoc.Pages)
                {
                    finalDoc.Pages.Add(p);
                }

                // Save the combined PDF to the requested output path
                finalDoc.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary directory
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup exceptions
            }
        }
    }
}
