using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ThumbnailStreamProvider : IStreamProvider
{
    private readonly Diagram _diagram;

    public ThumbnailStreamProvider(Diagram diagram)
    {
        _diagram = diagram;
    }

    // Called by Aspose when a resource stream is needed during HTML export
    public void InitStream(StreamProviderOptions options)
    {
        try
        {
            // Determine the page index from the default file name (e.g., "page_1.png")
            int pageIndex = 0;
            string fileName = Path.GetFileName(options.DefaultPath);
            Match match = Regex.Match(fileName, @"page_(\d+)\.png", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                // HTML export uses 1‑based naming; convert to zero‑based index
                pageIndex = int.Parse(match.Groups[1].Value) - 1;
                if (pageIndex < 0) pageIndex = 0;
            }

            // Generate a PNG thumbnail for the identified page
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.PageIndex = pageIndex;
            imgOptions.PageCount = 1;

            using (MemoryStream imageStream = new MemoryStream())
            {
                _diagram.Save(imageStream, imgOptions);
                imageStream.Position = 0;
                // Assign the generated stream to the provider options
                options.Stream = imageStream;
                // Note: Aspose will read the stream before we exit this method,
                // so we do not dispose the stream here.
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating thumbnail for resource '{options.DefaultPath}': {ex.Message}");
            // Ensure a non‑null stream to avoid downstream null reference
            options.Stream = new MemoryStream();
        }
    }

    // Called by Aspose after the resource has been written
    public void CloseStream(StreamProviderOptions options)
    {
        if (options.Stream != null)
        {
            options.Stream.Dispose();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page: generate a thumbnail PNG and an HTML file
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // ----- Generate thumbnail PNG for the index page -----
                string thumbPath = Path.Combine(outputFolder, $"page_{i + 1}.png");
                ImageSaveOptions imgOpts = new ImageSaveOptions(SaveFileFormat.Png);
                imgOpts.PageIndex = i;
                imgOpts.PageCount = 1;
                diagram.Save(thumbPath, imgOpts);

                // ----- Export HTML for the current page -----
                HTMLSaveOptions htmlOpts = new HTMLSaveOptions();
                htmlOpts.PageIndex = i;               // Export only the current page
                htmlOpts.PageCount = 1;
                htmlOpts.ExportHiddenPage = false;    // bool property
                htmlOpts.IsExportComments = false;    // bool property
                htmlOpts.StreamProvider = new ThumbnailStreamProvider(diagram);

                string htmlFileName = Path.Combine(outputFolder, $"page_{i + 1}.html");
                diagram.Save(htmlFileName, htmlOpts);
            }

            // ----- Create an index.html that embeds the thumbnails -----
            string indexPath = Path.Combine(outputFolder, "index.html");
            using (StreamWriter writer = new StreamWriter(indexPath))
            {
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html><head><meta charset=\"UTF-8\"><title>Diagram Thumbnails</title></head><body>");
                writer.WriteLine("<h1>Diagram Pages</h1>");

                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    string thumbFile = $"page_{i + 1}.png";
                    string pageHtml = $"page_{i + 1}.html";
                    writer.WriteLine("<div style=\"margin-bottom:20px;\">");
                    writer.WriteLine($"<a href=\"{pageHtml}\"><img src=\"{thumbFile}\" alt=\"Page {i + 1}\" style=\"width:200px;height:auto;\"/></a>");
                    writer.WriteLine($"<p>Page {i + 1}</p>");
                    writer.WriteLine("</div>");
                }

                writer.WriteLine("</body></html>");
            }

            Console.WriteLine("HTML export with thumbnails completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }
}