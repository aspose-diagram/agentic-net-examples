using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Examine the first page to decide which margin to apply
            Page firstPage = diagram.Pages[0];
            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            const double tolerance = 0.05; // inches tolerance for size comparison

            // A4 dimensions: 8.27" x 11.69"
            bool isA4 = Math.Abs(pageWidth - 8.27) < tolerance && Math.Abs(pageHeight - 11.69) < tolerance;
            // Letter dimensions: 8.5" x 11"
            bool isLetter = Math.Abs(pageWidth - 8.5) < tolerance && Math.Abs(pageHeight - 11.0) < tolerance;

            if (isA4)
            {
                diagram.HeaderFooter.HeaderMargin.Value = 0.2;
                Console.WriteLine("Header margin set to 0.2 inches for A4 page size.");
            }
            else if (isLetter)
            {
                diagram.HeaderFooter.HeaderMargin.Value = 0.3;
                Console.WriteLine("Header margin set to 0.3 inches for Letter page size.");
            }
            else
            {
                Console.WriteLine("Page size does not match A4 or Letter. No margin change applied.");
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
