using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioToPdfBatch
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = "input.vsdx";

            // Folder where individual page PDFs will be saved
            string outputFolder = "OutputPdfs";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Iterate through each page in the diagram
            int pageIndex = 0;
            foreach (Page page in diagram.Pages)
            {
                // Define the output PDF file name for the current page
                string pdfFile = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.pdf");

                // Configure PDF save options to render only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = pageIndex,   // Zero‑based index of the page to render
                    PageCount = 1,           // Render only one page
                    SplitMultiPages = false // Keep the page as a single PDF document
                };

                // Save the specific page as a PDF
                diagram.Save(pdfFile, pdfOptions);

                // Note: Aspose.Diagram does not provide a direct API to add bookmarks for each shape.
                // If bookmarks are required, further processing with a PDF manipulation library
                // would be needed after this step.

                pageIndex++;
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
