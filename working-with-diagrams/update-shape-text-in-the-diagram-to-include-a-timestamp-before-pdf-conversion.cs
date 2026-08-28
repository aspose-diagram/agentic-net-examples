using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Generate timestamp string
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Retrieve existing plain text (may be empty)
                        string existingText = shape.Text.Value.Text ?? string.Empty;

                        // Build new text with timestamp prefix
                        string newText = $"{timestamp} {existingText}".Trim();

                        // Clear current text collection and add the new text run
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