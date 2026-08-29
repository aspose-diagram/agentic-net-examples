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
                string inputPath = "input.vsdx";

                // Output directory for PDF pages
                string outputDir = "output";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify picture (foreign) shapes
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Enable simple drop shadow
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            // Shadow color (black)
                            shape.Fill.ShdwForegnd.Value = "#000000";
                            // Shadow transparency (30%)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;
                            // Shadow offset
                            shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                            shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                        }
                    }

                    // Prepare PDF save options for the current page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        // Ensure the format is set explicitly
                        SaveFormat = SaveFileFormat.Pdf,
                        // Export only the current page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Optional: set a default font to avoid missing glyphs
                        DefaultFont = "Arial"
                    };

                    // Build output file name (e.g., Page_1.pdf)
                    string outputPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.pdf");

                    // Save the diagram page as PDF
                    diagram.Save(outputPath, pdfOptions);

                    pageIndex++;
                }

                // Cleanup
                diagram.Dispose();

                Console.WriteLine("Processing completed. PDFs are saved in: " + Path.GetFullPath(outputDir));

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }