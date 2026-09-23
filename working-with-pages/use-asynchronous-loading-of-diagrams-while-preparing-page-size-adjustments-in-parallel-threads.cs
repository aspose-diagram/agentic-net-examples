using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static async Task Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Asynchronously load the Visio diagram
        Diagram diagram = await Task.Run(() => new Diagram(inputPath));

        // Desired page size (A4) in inches
        double a4Width = 8.27;
        double a4Height = 11.69;

        // Collect pages into a list because PageCollection is not generic
        List<Page> pages = new List<Page>();
        foreach (Page p in diagram.Pages)
        {
            pages.Add(p);
        }

        // Adjust each page size in parallel
        Parallel.ForEach(pages, page =>
        {
            page.PageSheet.PageProps.PageWidth.Value = a4Width;
            page.PageSheet.PageProps.PageHeight.Value = a4Height;
        });

        // Save the modified diagram back to a Visio file
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
    }
}
