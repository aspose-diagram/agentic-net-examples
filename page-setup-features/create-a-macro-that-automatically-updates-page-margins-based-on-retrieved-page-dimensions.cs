using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output Visio file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and adjust margins proportionally to page size
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve current page width and height (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Example: set each margin to 5% of the corresponding dimension
                    double leftMargin = pageWidth * 0.05;
                    double rightMargin = pageWidth * 0.05;
                    double topMargin = pageHeight * 0.05;
                    double bottomMargin = pageHeight * 0.05;

                    // Update the print margins (values are in inches)
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