using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Properties;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";
                // Path for the exported PDF
                string outputPath = "output.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Set a custom document title
                    diagram.DocumentProps.Title = "My Custom Document Title";

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Embed fonts by specifying a default fallback font (Aspose.Diagram embeds fonts automatically when possible)
                        DefaultFont = "Arial",
                        // Include hidden pages in the PDF output
                        ExportHiddenPage = true,
                        // Ensure the save format is explicitly set (optional but clear)
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Save the diagram as PDF with the configured options
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("PDF export completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }