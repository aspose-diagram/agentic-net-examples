using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for the source diagram and the modified output
            string inputPath = "input.vsdx";
            string outputPath = "output_square.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages and make each page square (height = width)
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    page.PageSheet.PageProps.PageHeight.Value = width;
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
