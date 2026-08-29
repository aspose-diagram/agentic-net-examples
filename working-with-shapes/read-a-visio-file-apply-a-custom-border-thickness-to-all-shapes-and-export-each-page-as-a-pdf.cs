using System;
using System.IO;
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

                // Output directory for PDF files
                string outputDir = "ExportedPdfs";

                // Ensure the output directory exists
                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the custom border thickness (in inches)
                double customBorderThickness = 0.02; // Adjust as needed

                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Apply the custom border thickness to all non-deleted shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set line weight (border thickness)
                        shape.Line.LineWeight.Value = customBorderThickness;
                    }

                    // Prepare PDF save options for the current page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Export only the current page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Explicitly set the save format
                        SaveFormat = SaveFileFormat.Pdf,
                        // Optional: set a default font to avoid missing font issues
                        DefaultFont = "Arial"
                    };

                    // Build the output PDF file name
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.pdf");

                    // Save the diagram (only the specified page) as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Processing completed. PDFs are saved in: " + Path.GetFullPath(outputDir));

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }