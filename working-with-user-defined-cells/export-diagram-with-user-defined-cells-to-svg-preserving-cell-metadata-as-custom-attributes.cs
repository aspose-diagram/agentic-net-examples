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

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to read user‑defined cells
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    foreach (User userCell in shape.Users)
                    {
                        Console.WriteLine($"Shape ID {shape.ID}, User Cell '{userCell.NameU}': Value = {userCell.Value.Val}");
                    }
                }
            }

            // Configure SVG save options – these settings help preserve metadata as custom attributes
            SVGSaveOptions svgOptions = new SVGSaveOptions();
            svgOptions.ExportHiddenPage = false;
            svgOptions.ExportGuideShapes = false;
            svgOptions.SVGFitToViewPort = true;
            svgOptions.ExportElementAsRectTag = true;

            // Export the entire diagram to SVG using the configured options
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
