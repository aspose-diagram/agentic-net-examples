using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> [output.pdf]");
            return;
        }

        string diagramPath = args[0];
        string outputPdfPath = args.Length > 1 ? args[1] : "ShapesReport.pdf";

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file not found: {diagramPath}");
            return;
        }

        // Temporary folder for shape images.
        string tempImageFolder = Path.Combine(Path.GetTempPath(), "ShapeImages_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempImageFolder);

        // List to keep image streams open until PDF is saved.
        List<FileStream> openImageStreams = new List<FileStream>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Create a new PDF document (fully qualified to avoid namespace clash).
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Iterate through each page and each shape.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Prepare metadata.
                    string shapeId = shape.ID.ToString();
                    string shapeName = shape.Name ?? "N/A";
                    string masterName = shape.Master != null ? shape.Master.Name : "N/A";

                    // Export the shape to a PNG image.
                    string imagePath = Path.Combine(tempImageFolder, $"shape_{shapeId}.png");
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    shape.ToImage(imagePath, imgOptions);

                    // Open the image file stream.
                    FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                    openImageStreams.Add(imgStream);

                    // Add a new page to the PDF for this shape.
                    Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                    // Insert the image into the PDF page.
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.ImageStream = imgStream;
                    // Position the image at the top‑left corner.
                    pdfImage.FixHeight = 400; // Adjust as needed.
                    pdfImage.FixWidth = 400;
                    pdfPage.Paragraphs.Add(pdfImage);

                    // Add metadata text below the image.
                    string metaText = $"Shape ID: {shapeId}\nName: {shapeName}\nMaster: {masterName}";
                    Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(metaText);
                    // Position the text fragment just below the image.
                    tf.Position = new Aspose.Pdf.Text.Position(0, pdfImage.FixHeight + 10);
                    pdfPage.Paragraphs.Add(tf);
                }
            }

            // Save the assembled PDF report.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF report generated successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred during processing:");
            Console.Error.WriteLine(ex.Message);
        }
        finally
        {
            // Ensure all opened image streams are closed.
            foreach (var stream in openImageStreams)
            {
                try { stream.Close(); } catch { /* ignore */ }
            }

            // Clean up temporary images.
            try
            {
                if (Directory.Exists(tempImageFolder))
                {
                    Directory.Delete(tempImageFolder, true);
                }
            }
            catch (Exception cleanupEx)
            {
                Console.Error.WriteLine("Failed to clean up temporary files:");
                Console.Error.WriteLine(cleanupEx.Message);
            }
        }
    }
}