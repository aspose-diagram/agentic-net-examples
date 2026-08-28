using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the exported PDF file
                string outputPath = "output.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Set a custom document title
                    diagram.DocumentProps.Title = "My Custom Document Title";

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure hidden pages are exported
                        ExportHiddenPage = true,
                        // Set a default font to be used for missing fonts
                        DefaultFont = "Arial",
                        // Explicitly set the save format (required by the API)
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Save the diagram as PDF with the specified options
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram has been exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }