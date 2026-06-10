using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram original = new Diagram(inputPath);
            int pageCount = original.Pages.Count;

            for (int i = 0; i < pageCount; i++)
            {
                // Load a fresh copy for each page to avoid mutating the original diagram
                Diagram pageDiagram = new Diagram(inputPath);

                // Collect pages that are not the target page
                List<Page> pagesToRemove = new List<Page>();
                for (int j = 0; j < pageDiagram.Pages.Count; j++)
                {
                    if (j != i)
                        pagesToRemove.Add(pageDiagram.Pages[j]);
                }

                // Remove the unwanted pages
                foreach (Page p in pagesToRemove)
                {
                    pageDiagram.Pages.Remove(p);
                }

                // Resize the remaining page (example size: 8.5 x 11 inches)
                Page page = pageDiagram.Pages[0];
                page.PageSheet.PageProps.PageWidth.Value = 8.5;
                page.PageSheet.PageProps.PageHeight.Value = 11;

                // Save the single‑page diagram
                string outputPath = $"Page_{i}.vsdx";
                pageDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Exported page {i} to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}