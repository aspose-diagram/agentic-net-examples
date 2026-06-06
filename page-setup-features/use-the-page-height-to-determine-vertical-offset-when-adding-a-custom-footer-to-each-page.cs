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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";   // TODO: replace with actual file path
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Determine vertical offset as 5% of the page height
                    double footerOffset = pageHeight * 0.05; // distance from bottom edge

                    // Define footer text shape dimensions
                    double footerHeight = 0.2; // height of the footer text box (in inches)
                    double footerWidth = pageWidth; // span the full page width

                    // Add a text shape at the calculated offset
                    // PinX = 0 (left edge), PinY = footerOffset (distance from bottom)
                    page.AddText(0, footerOffset, footerWidth, footerHeight, "Custom Footer");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx"; // TODO: replace with desired output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
