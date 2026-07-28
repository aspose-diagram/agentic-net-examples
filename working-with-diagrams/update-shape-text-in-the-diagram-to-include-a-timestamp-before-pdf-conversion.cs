using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPdfPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Generate timestamp string
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve existing plain text
                        string originalText = shape.Text.Value.Text;

                        // Prepare new text with timestamp prefix
                        string newText = $"{timestamp} {originalText}";

                        // Clear existing text runs
                        shape.Text.Value.Clear();

                        // Add the new text as a single Txt run
                        shape.Text.Value.Add(new Txt(newText));
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure the format is explicitly set (optional but safe)
                    SaveFormat = SaveFileFormat.Pdf,
                    // Use a default font to avoid missing font issues
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF with the updated text
                diagram.Save(outputPdfPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }