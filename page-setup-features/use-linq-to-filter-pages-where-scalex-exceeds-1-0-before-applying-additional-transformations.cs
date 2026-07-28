using System;
using System.Collections.Generic;
using System.Linq;
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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Filter pages where the print scaling factor ScaleX exceeds 1.0
                List<Page> filteredPages = diagram.Pages
                    .Cast<Page>()
                    .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                    .ToList();

                // Apply additional transformations to the filtered pages
                foreach (Page page in filteredPages)
                {
                    // Example transformation: set page size to Letter (11" x 8.5")
                    page.PageSheet.PageProps.PageWidth.Value = 11.0;
                    page.PageSheet.PageProps.PageHeight.Value = 8.5;
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