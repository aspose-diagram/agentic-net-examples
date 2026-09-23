using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Base footer margin (in inches)
            double baseMargin = 0.5;

            // Adjust the footer margin for each page based on the number of shapes it contains
            foreach (Page page in diagram.Pages)
            {
                // Count the shapes on the current page
                int shapeCount = 0;
                foreach (Shape shape in page.Shapes)
                {
                    shapeCount++;
                }

                // Example logic: add 0.01 inch for each shape to avoid overlap
                double additionalMargin = shapeCount * 0.01;

                // Set the global footer margin (applies to all pages)
                diagram.HeaderFooter.FooterMargin.Value = baseMargin + additionalMargin;
            }

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
