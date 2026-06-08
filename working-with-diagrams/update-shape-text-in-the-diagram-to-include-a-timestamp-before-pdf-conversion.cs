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

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Create a timestamp string
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current plain text of the shape
                    string existingText = shape.Text.Value.Text;

                    // Build the new text with the timestamp prefixed
                    string newText = timestamp + " " + existingText;

                    // Replace the shape's text content
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(newText));
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            pdfOptions.DefaultFont = "Arial";

            // Save the updated diagram as PDF
            string outputPath = "output.pdf";
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
