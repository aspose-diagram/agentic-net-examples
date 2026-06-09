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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages to find the 'Details' layer and hide it
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection of the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare the layer name (Str2Value) with the target name
                        if (layer.Name.Value == "Details")
                        {
                            // Hide the layer
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure hidden pages are not exported (layer visibility already handled)
                    ExportHiddenPage = false,
                    // Optional: set a default font to avoid missing font issues
                    DefaultFont = "Arial"
                };

                // Export the diagram to PDF
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }