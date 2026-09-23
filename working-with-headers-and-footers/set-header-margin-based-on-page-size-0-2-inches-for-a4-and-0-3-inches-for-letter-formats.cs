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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the first page defines the document size
            if (diagram.Pages.Count == 0)
            {
                throw new Exception("The diagram contains no pages.");
            }

            Page firstPage = diagram.Pages[0];
            double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

            // Determine page format and set header margin accordingly
            // A4: 8.27 x 11.69 inches -> 0.2 inch margin
            // Letter: 8.5 x 11 inches -> 0.3 inch margin
            const double a4Width = 8.27;
            const double a4Height = 11.69;
            const double letterWidth = 8.5;
            const double letterHeight = 11.0;

            if (Math.Abs(pageWidth - a4Width) < 0.01 && Math.Abs(pageHeight - a4Height) < 0.01)
            {
                diagram.HeaderFooter.HeaderMargin.Value = 0.2;
            }
            else if (Math.Abs(pageWidth - letterWidth) < 0.01 && Math.Abs(pageHeight - letterHeight) < 0.01)
            {
                diagram.HeaderFooter.HeaderMargin.Value = 0.3;
            }
            else
            {
                // Default margin for other sizes
                diagram.HeaderFooter.HeaderMargin.Value = 0.25;
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
