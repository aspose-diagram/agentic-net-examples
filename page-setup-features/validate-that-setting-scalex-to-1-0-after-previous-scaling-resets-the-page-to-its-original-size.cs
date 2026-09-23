using System.IO;
using System;
using Aspose.Diagram;

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
                // Access the first page
                Page page = diagram.Pages[0];

                // Store original page dimensions (in inches)
                double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Apply a temporary scaling factor
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                page.PageSheet.PrintProps.ScaleY.Value = 0.5;

                // Reset scaling to original (1.0)
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                page.PageSheet.PrintProps.ScaleY.Value = 1.0;

                // Verify that page dimensions are unchanged
                double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
                const double tolerance = 1e-6;

                if (Math.Abs(originalWidth - currentWidth) > tolerance ||
                    Math.Abs(originalHeight - currentHeight) > tolerance)
                {
                    throw new Exception("Page size changed after resetting ScaleX/ScaleY.");
                }

                // Save the diagram to confirm changes
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
