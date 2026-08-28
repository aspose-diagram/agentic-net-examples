using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Filter pages where the print scaling factor ScaleX exceeds 1.0
                    var pagesToTransform = diagram.Pages
                        .Cast<Page>()
                        .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                        .ToList();

                    // Apply additional transformations to each filtered page
                    foreach (var page in pagesToTransform)
                    {
                        // Example transformation: set page dimensions to standard Letter size (11" x 8.5")
                        page.PageSheet.PageProps.PageWidth.Value = 11.0;
                        page.PageSheet.PageProps.PageHeight.Value = 8.5;
                    }

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }