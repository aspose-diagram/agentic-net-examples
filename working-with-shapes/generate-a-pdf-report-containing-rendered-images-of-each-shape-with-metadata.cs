using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument) and output PDF path (second argument)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPdfPath = args.Length > 1 ? args[1] : "ShapeReport.pdf";

        // Guard against missing input file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Create a new PDF document (fully qualified to avoid namespace ambiguity)
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare a temporary PNG file for the shape image
                        string tempImagePath = Path.Combine(Path.GetTempPath(),
                            $"shape_{shape.ID}_{Guid.NewGuid()}.png");

                        // Export the shape to PNG using ImageSaveOptions
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        shape.ToImage(tempImagePath, imgOptions);

                        // Add a new page to the PDF for this shape
                        Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                        // Insert the shape image into the PDF page
                        using (FileStream imgStream = File.OpenRead(tempImagePath))
                        {
                            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                            {
                                ImageStream = imgStream,
                                // Scale the image to fit within the page width (leaving margins)
                                FixWidth = pdfPage.PageInfo.Width - 40
                            };
                            pdfPage.Paragraphs.Add(pdfImage);
                        }

                        // Delete the temporary image file
                        try { File.Delete(tempImagePath); } catch { /* ignore cleanup errors */ }

                        // Build metadata string for the shape
                        string masterName = shape.Master != null ? shape.Master.Name : "N/A";
                        string shapeName = !string.IsNullOrWhiteSpace(shape.Name) ? shape.Name : "N/A";
                        string metadata = $"Shape ID: {shape.ID}, Name: {shapeName}, Master: {masterName}";

                        // Add metadata as a text fragment below the image
                        Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment(metadata);
                        textFragment.Margin = new Aspose.Pdf.MarginInfo { Top = 10, Bottom = 10 };
                        pdfPage.Paragraphs.Add(textFragment);
                    }
                }

                // Save the assembled PDF report
                pdfDocument.Save(outputPdfPath);
                Console.WriteLine($"PDF report generated successfully at '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}