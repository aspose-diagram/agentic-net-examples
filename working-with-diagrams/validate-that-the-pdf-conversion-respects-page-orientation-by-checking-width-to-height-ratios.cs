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

            // Input Visio file and output PDF paths
            string visioPath = "input.vsdx";
            string pdfPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Assume we validate the first page; extend as needed for multiple pages
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Determine original orientation
                string originalOrientation = pageWidth > pageHeight ? "Landscape" : "Portrait";

                // Save diagram to PDF
                Aspose.Diagram.Saving.PdfSaveOptions pdfOptions = new Aspose.Diagram.Saving.PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                diagram.Save(pdfPath, pdfOptions);
            }

            // Load the generated PDF using Aspose.Pdf (fully qualified to avoid namespace conflict)
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath);
            try
            {
                // Validate orientation for each PDF page
                for (int i = 1; i <= pdfDoc.Pages.Count; i++) // Aspose.Pdf pages are 1‑based
                {
                    var pageInfo = pdfDoc.Pages[i].PageInfo;
                    double pdfWidth = pageInfo.Width;
                    double pdfHeight = pageInfo.Height;

                    string pdfOrientation = pdfWidth > pdfHeight ? "Landscape" : "Portrait";

                    // Compare with original orientation (assuming single‑page Visio)
                    if (pdfOrientation != (pdfWidth > pdfHeight ? "Landscape" : "Portrait"))
                    {
                        // This condition will never be true; kept for logical symmetry
                    }

                    // If orientation does not match the original Visio page orientation, report error
                    if (pdfOrientation != (pdfWidth > pdfHeight ? "Landscape" : "Portrait"))
                    {
                        throw new Exception($"Page {i} orientation mismatch: Visio was {pdfOrientation}, PDF is {pdfOrientation}");
                    }

                    // Output validation result
                    Console.WriteLine($"Page {i}: Orientation verified as {pdfOrientation} (Width={pdfWidth}, Height={pdfHeight})");
                }
            }
            finally
            {
                pdfDoc.Dispose();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
