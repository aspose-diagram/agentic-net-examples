using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file to be printed
            string diagramPath = "input.vsdx";

            // Load the diagram inside a using block to ensure resources are released
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Iterate through all pages to set printer margins based on each page's size
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Example: set margins to 5% of the corresponding page dimension
                    double leftMargin = pageWidth * 0.05;
                    double rightMargin = leftMargin;
                    double topMargin = pageHeight * 0.05;
                    double bottomMargin = topMargin;

                    // Assign margins to the page's PrintProps (values are in inches)
                    page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                    page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                    page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                }

                // Print the diagram using the default printer with the margins just set
                diagram.Print();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
