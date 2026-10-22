using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_modified.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Update print settings for each page
            foreach (Page page in diagram.Pages)
            {
                // Access the PrintProps object for the page
                PrintProps printProps = page.PageSheet.PrintProps;

                // Example: set scaling factors (1 = 100%)
                printProps.ScaleX.Value = 1.0;
                printProps.ScaleY.Value = 1.0;

                // Example: set page margins (in inches)
                printProps.PageLeftMargin.Value = 0.5;
                printProps.PageRightMargin.Value = 0.5;
                printProps.PageTopMargin.Value = 0.5;
                printProps.PageBottomMargin.Value = 0.5;

                // Example: set orientation to portrait (true = portrait, false = landscape)
                // Note: In Aspose.Diagram, orientation is controlled via the PageSheet's PageProps.
                // Here we set the page width greater than height for landscape, otherwise portrait.
                // Adjust as needed.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;
                if (pageWidth > pageHeight)
                {
                    // Switch to portrait by swapping width and height
                    page.PageSheet.PageProps.PageWidth.Value = pageHeight;
                    page.PageSheet.PageProps.PageHeight.Value = pageWidth;
                }
            }

            // Save the modified diagram to a new VSDX file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}