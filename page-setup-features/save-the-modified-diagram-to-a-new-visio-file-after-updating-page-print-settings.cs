using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_modified.vsdx";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages and update print settings
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the PrintProps collection
                        var printProps = page.PageSheet.PrintProps;

                        // Set orientation to Landscape
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set scaling to 75%
                        printProps.ScaleX.Value = 0.75;
                        printProps.ScaleY.Value = 0.75;

                        // Enable Fit to Sheet and define one sheet across and down
                        printProps.OnPage.Value = BOOL.True;
                        printProps.PagesX.Value = 1;
                        printProps.PagesY.Value = 1;

                        // Set margins (in inches)
                        printProps.PageTopMargin.Value = 0.5;    // 0.5 inch top margin
                        printProps.PageBottomMargin.Value = 0.5; // 0.5 inch bottom margin
                        printProps.PageLeftMargin.Value = 0.5;   // 0.5 inch left margin
                        printProps.PageRightMargin.Value = 0.5;  // 0.5 inch right margin
                    }

                    // Save the modified diagram to a new file
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
                catch (Exception ex)
                {
                    // Simple error handling
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }