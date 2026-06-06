using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

namespace DiagramUtilities
{
    /// <summary>
    /// Service that applies a set of default print settings to every page of a Diagram.
    /// </summary>
    public class DiagramPrintSettingsService
    {
        /// <summary>
        /// Applies default print configuration to all pages of the provided diagram.
        /// </summary>
        /// <param name="diagram">The Diagram instance to modify.</param>
        public void ApplyDefaultPrintSettings(Diagram diagram)
        {
            if (diagram == null)
                throw new ArgumentNullException(nameof(diagram));

            // Iterate through each page and set the print properties.
            foreach (Page page in diagram.Pages)
            {
                var printProps = page.PageSheet.PrintProps;

                // Orientation: Portrait
                printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                // Scaling: 100% (no scaling)
                printProps.ScaleX.Value = 1.0;
                printProps.ScaleY.Value = 1.0;

                // Fit to sheet: enable and set 1x1 pages per sheet
                printProps.OnPage.Value = BOOL.True;
                printProps.PagesX.Value = 1;
                printProps.PagesY.Value = 1;

                // Margins: 0.5 inches on each side
                const double marginInches = 0.5;
                printProps.PageTopMargin.Value = marginInches;
                printProps.PageBottomMargin.Value = marginInches;
                printProps.PageLeftMargin.Value = marginInches;
                printProps.PageRightMargin.Value = marginInches;
            }
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "example.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Apply default print settings
                var printService = new DiagramPrintSettingsService();
                printService.ApplyDefaultPrintSettings(diagram);

                // Save the diagram (output path can be the same or different)
                string outputPath = "example_modified.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}