using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = @"C:\Diagrams\Sample.vsdx";

            // Path for the resulting PDF file
            string outputPdf = @"C:\Diagrams\Sample_renamed.pdf";

            // Create a prefix from the file name (without extension)
            string prefix = Path.GetFileNameWithoutExtension(inputPath) + "_";

            // Load the diagram using the Diagram(string) constructor
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Rename the shape by adding the derived prefix
                        shape.Name = prefix + shape.Name;
                    }
                }

                // Export the modified diagram to PDF using the Save method with SaveFileFormat.Pdf
                diagram.Save(outputPdf, SaveFileFormat.Pdf);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
