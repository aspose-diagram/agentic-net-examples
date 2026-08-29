using System;
using System.IO;
using Aspose.Diagram;

class VisioToPdfPerShape
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string visioPath = @"C:\VisioFiles\input.vsdx";

            // Folder where individual shape PDFs will be saved
            string outputFolder = @"C:\VisioFiles\ShapePdfs";

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram (uses the Diagram(string) constructor)
            using (Diagram diagram = new Diagram(visioPath))
            {
                int pageIdx = 0;

                // Iterate through each page in the document
                foreach (Page page in diagram.Pages)
                {
                    int shapeIdx = 0;

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a file name that reflects page and shape order
                        // This preserves the original layer/shape ordering in the file names
                        string pdfFile = Path.Combine(
                            outputFolder,
                            $"Page{pageIdx:D2}_Shape{shapeIdx:D4}.pdf");

                        // Convert the shape to a vector PDF page
                        shape.ToPdf(pdfFile);

                        shapeIdx++;
                    }

                    pageIdx++;
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
