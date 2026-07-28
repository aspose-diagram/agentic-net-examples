using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Cache to store page dimensions (width and height) keyed by page ID
                var pageSizeCache = new Dictionary<int, (double Width, double Height)>();

                // First pass: read page size values and store them in the cache
                foreach (Page page in diagram.Pages)
                {
                    int pageId = page.ID;
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    pageSizeCache[pageId] = (width, height);
                }

                // Example usage: apply cached sizes back to pages without re‑reading the properties
                foreach (Page page in diagram.Pages)
                {
                    var cachedSize = pageSizeCache[page.ID];
                    page.PageSheet.PageProps.PageWidth.Value = cachedSize.Width;
                    page.PageSheet.PageProps.PageHeight.Value = cachedSize.Height;
                }

                // Save the diagram with the updated page sizes
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
