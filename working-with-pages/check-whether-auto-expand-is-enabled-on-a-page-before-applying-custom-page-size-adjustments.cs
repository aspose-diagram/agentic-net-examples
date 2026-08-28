using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file (you can modify or obtain from user input)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Desired custom page size in inches (example: A4 size)
                double customWidth = 8.27;
                double customHeight = 11.69;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Check the auto‑expand (DrawingResizeType) setting
                    var resizeType = page.PageSheet.PageProps.DrawingResizeType.Value;

                    if (resizeType == DrawingResizeTypeValue.Automatically)
                    {
                        Console.WriteLine($"Page \"{page.Name}\" has auto‑expand enabled. Skipping size adjustment.");
                        continue; // Skip size change for this page
                    }

                    // Auto‑expand is disabled; apply custom size
                    page.PageSheet.PageProps.PageWidth.Value = customWidth;
                    page.PageSheet.PageProps.PageHeight.Value = customHeight;
                    Console.WriteLine($"Page \"{page.Name}\" size set to {customWidth} x {customHeight} inches.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
