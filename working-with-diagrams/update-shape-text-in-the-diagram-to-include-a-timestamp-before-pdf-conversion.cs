using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output PDF file
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Generate a timestamp string
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Iterate through all pages and shapes, updating text
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve existing plain text (may be empty)
                    string existingText = shape.Text.Value.Text;

                    // Build new text with timestamp prefix
                    string newText = string.IsNullOrWhiteSpace(existingText)
                        ? timestamp
                        : $"{timestamp} {existingText}";

                    // Clear current text runs and add the new text
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(newText));
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
