using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output folder for thumbnails
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Iterate through each page and generate a thumbnail
        int pageIndex = 0;
        foreach (Page page in diagram.Pages)
        {
            // Build the thumbnail file name (e.g., Page_1.png)
            string fileName = $"Page_{pageIndex + 1}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            // Configure image save options for PNG thumbnail
            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
            options.PageIndex = pageIndex;   // zero‑based page index
            options.PageCount = 1;           // export only the current page
            options.Resolution = 96;         // DPI for the thumbnail
            options.Scale = 0.2f;            // scale down to 20% of original size

            // Save the thumbnail image
            diagram.Save(outputPath, options);

            Console.WriteLine($"Thumbnail saved: {outputPath}");

            pageIndex++;
        }
    }
}
