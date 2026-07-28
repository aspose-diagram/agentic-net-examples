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
            string inputPath = "input.vsdx";
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (in inches)
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define vertical spacing as 10% of the page height
                    double verticalSpacing = pageHeight * 0.10;

                    // Apply the calculated spacing to the page layout.
                    // AvenueSizeY controls the vertical distance between shapes when auto‑layout is used.
                    page.PageSheet.PageLayout.AvenueSizeY.Value = verticalSpacing;
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Legend spacing updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
