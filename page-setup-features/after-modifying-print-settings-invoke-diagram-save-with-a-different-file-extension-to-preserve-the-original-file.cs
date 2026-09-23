using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (original diagram)
                string inputPath = "input.vsdx";

                // Output file path with a different extension (e.g., PDF) to preserve the original file
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and modify print settings
                foreach (Page page in diagram.Pages)
                {
                    // Access the print properties of the page
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Set orientation to Landscape
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set scaling to 75%
                    printProps.ScaleX.Value = 0.75;
                    printProps.ScaleY.Value = 0.75;

                    // Enable fit to sheet (print on a single page)
                    printProps.OnPage.Value = BOOL.True;
                    printProps.PagesX.Value = 1;
                    printProps.PagesY.Value = 1;

                    // Set page margins (in inches)
                    printProps.PageTopMargin.Value = 0.5;
                    printProps.PageBottomMargin.Value = 0.5;
                    printProps.PageLeftMargin.Value = 0.5;
                    printProps.PageRightMargin.Value = 0.5;
                }

                // Save the modified diagram to a different format (PDF) to keep the original file unchanged
                diagram.Save(outputPath, SaveFileFormat.Pdf);

                Console.WriteLine("Print settings updated and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }