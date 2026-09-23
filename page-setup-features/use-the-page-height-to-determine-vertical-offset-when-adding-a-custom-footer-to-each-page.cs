using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Determine a vertical offset for the footer based on each page's height
            // Here we use 5% of the page height as the footer margin
            foreach (Page page in diagram.Pages)
            {
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value; // height in inches
                double footerOffset = pageHeight * 0.05; // 5% offset
                diagram.HeaderFooter.FooterMargin.Value = footerOffset;
            }

            // Set the footer text to include automatic page numbering
            diagram.HeaderFooter.FooterRight = "Page: &p";

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
