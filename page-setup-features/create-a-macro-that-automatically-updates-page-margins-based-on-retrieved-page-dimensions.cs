using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Update margins for each page based on its dimensions
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions are in inches
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Example: set each margin to 5 % of the corresponding dimension
                double leftMargin = pageWidth * 0.05;
                double rightMargin = pageWidth * 0.05;
                double topMargin = pageHeight * 0.05;
                double bottomMargin = pageHeight * 0.05;

                // Apply the margins via the PrintProps collection
                page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
