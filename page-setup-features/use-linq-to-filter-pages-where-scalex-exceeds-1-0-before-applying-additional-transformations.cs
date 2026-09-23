using System;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Filter pages where the print scaling factor ScaleX exceeds 1.0
                List<Page> filteredPages = diagram.Pages
                    .Cast<Page>()
                    .Where(p => p.PageSheet.PrintProps.ScaleX.Value > 1.0)
                    .ToList();

                // Apply additional transformations to the filtered pages
                foreach (Page page in filteredPages)
                {
                    // Example transformation: reset ScaleX and ScaleY to 1.0
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                    page.PageSheet.PrintProps.ScaleY.Value = 1.0;
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }