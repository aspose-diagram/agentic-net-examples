using System;
using System.IO;
using Aspose.Diagram;

class ShapeToPdfConverter
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string visioFile = "input.vsdx";

            // Folder where individual shape PDFs will be saved
            string outputFolder = "ShapePdfPages";
            Directory.CreateDirectory(outputFolder);

            // Load the Visio diagram (uses Diagram(string) constructor)
            using (Diagram diagram = new Diagram(visioFile))
            {
                // Iterate through each page in the document
                foreach (Page page in diagram.Pages)
                {
                    // Shapes are enumerated in the order they appear on the page,
                    // which preserves the original layer ordering.
                    int shapeCounter = 0;
                    foreach (Shape shape in page.Shapes)
                    {
                        // Construct a unique PDF file name for the shape
                        string pdfFile = Path.Combine(
                            outputFolder,
                            $"{page.Name}_Shape{shape.ID}_{shapeCounter}.pdf");

                        // Convert the shape to a vector PDF page (uses Shape.ToPdf(string))
                        shape.ToPdf(pdfFile);

                        shapeCounter++;
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
