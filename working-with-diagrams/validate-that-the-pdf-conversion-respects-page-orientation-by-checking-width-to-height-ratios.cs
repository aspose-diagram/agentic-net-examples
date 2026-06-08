using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramOrientationValidator
{
    // Custom callback to validate page orientation during PDF saving
    public class OrientationValidationCallback : IPageSavingCallback
    {
        private readonly Diagram _diagram;

        public OrientationValidationCallback(Diagram diagram)
        {
            _diagram = diagram;
        }

        // Called before each page is saved
        public void PageStartSaving(PageStartSavingArgs args)
        {
            // Retrieve the page being saved
            Page page = _diagram.Pages[args.PageIndex];

            // Get page dimensions (in inches)
            double width = page.PageSheet.PageProps.PageWidth.Value;
            double height = page.PageSheet.PageProps.PageHeight.Value;

            // Determine orientation set in the page's print properties
            PrintPageOrientationValue orientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

            // Validate width‑to‑height ratio against the orientation
            bool isLandscape = width > height;
            bool orientationMatches = (orientation == PrintPageOrientationValue.Landscape && isLandscape) ||
                                      (orientation == PrintPageOrientationValue.Portrait && !isLandscape);

            if (!orientationMatches)
            {
                string message = $"Page {args.PageIndex + 1} orientation mismatch: " +
                                 $"Print orientation is {orientation}, " +
                                 $"but dimensions are Width={width:F2}\" Height={height:F2}\".";
                throw new Exception(message);
            }
        }

        // Called after each page is saved (no validation needed here)
        public void PageEndSaving(PageEndSavingArgs args)
        {
            // No action required
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Path for the generated PDF
                string outputPdf = "output.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(sourcePath))
                {
                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial"; // fallback font
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf; // ensure correct format

                    // Assign the custom orientation validation callback
                    pdfOptions.PageSavingCallback = new OrientationValidationCallback(diagram);

                    // Save the diagram as PDF; the callback will validate each page
                    diagram.Save(outputPdf, pdfOptions);
                }

                Console.WriteLine("PDF conversion completed and orientation validated successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}