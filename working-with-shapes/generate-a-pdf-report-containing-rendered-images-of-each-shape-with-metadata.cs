using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputVisioPath = "input.vsdx";
            // Output PDF report path
            string outputPdfPath = "Report.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputVisioPath);

            // Create a new PDF document (Aspose.Pdf types are fully qualified to avoid ambiguity)
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

            // Iterate through each page in the diagram
            foreach (Page diagramPage in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in diagramPage.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Generate a temporary PNG file for the shape
                    string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    shape.ToImage(tempImagePath, imgOptions);

                    // Add a new page to the PDF for this shape
                    Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                    // Insert the rendered shape image into the PDF page
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.ImageStream = new FileStream(tempImagePath, FileMode.Open, FileAccess.Read);
                    pdfPage.Paragraphs.Add(pdfImage);

                    // Prepare metadata text for the shape
                    string masterName = shape.Master != null ? shape.Master.Name : "N/A";
                    string metadata = $"Shape ID: {shape.ID}, NameU: {shape.NameU}, Master: {masterName}";

                    // Insert metadata text below the image
                    Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment(metadata);
                    textFragment.TextState.FontSize = 12;
                    pdfPage.Paragraphs.Add(textFragment);

                    // Clean up the temporary image file
                    pdfImage.ImageStream.Close();
                    File.Delete(tempImagePath);
                }
            }

            // Save the assembled PDF report
            pdfDocument.Save(outputPdfPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
