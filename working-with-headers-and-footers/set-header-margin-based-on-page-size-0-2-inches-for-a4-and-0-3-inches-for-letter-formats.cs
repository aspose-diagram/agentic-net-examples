using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Default header margin (in inches)
            double headerMargin = 0.0;

            // Determine page size based on the first page (Visio pages are uniform in size)
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

                // A4 size approx. 8.27 x 11.69 inches
                bool isA4 = Math.Abs(pageWidth - 8.27) < 0.01 && Math.Abs(pageHeight - 11.69) < 0.01;
                // Letter size approx. 8.5 x 11 inches
                bool isLetter = Math.Abs(pageWidth - 8.5) < 0.01 && Math.Abs(pageHeight - 11.0) < 0.01;

                if (isA4)
                    headerMargin = 0.2;   // 0.2 inches for A4
                else if (isLetter)
                    headerMargin = 0.3;   // 0.3 inches for Letter
                else
                    headerMargin = 0.2;   // Fallback value
            }

            // Apply the calculated header margin globally
            diagram.HeaderFooter.HeaderMargin.Value = headerMargin;

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
