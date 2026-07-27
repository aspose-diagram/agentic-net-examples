using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class ThumbnailStreamProvider : IStreamProvider
{
    private readonly Diagram _diagram;

    public ThumbnailStreamProvider(Diagram diagram)
    {
        _diagram = diagram ?? throw new ArgumentNullException(nameof(diagram));
    }

    // Called by Aspose when an external resource (e.g., an image) is requested during HTML export
    public void InitStream(StreamProviderOptions options)
    {
        // Determine which page the image belongs to from the default path (e.g., "page1.png")
        int pageIndex = 0; // zero‑based
        if (!string.IsNullOrEmpty(options.DefaultPath))
        {
            Match m = Regex.Match(options.DefaultPath, @"page(\d+)", RegexOptions.IgnoreCase);
            if (m.Success && int.TryParse(m.Groups[1].Value, out int pageNumber))
            {
                pageIndex = Math.Max(0, pageNumber - 1);
            }
        }

        // Configure image export options for a thumbnail
        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
        imgOptions.PageIndex = pageIndex;
        imgOptions.PageCount = 1;
        imgOptions.Scale = 0.2f; // 20 % of original size

        // Render the page to a memory stream and assign it to the provider
        MemoryStream ms = new MemoryStream();
        _diagram.Save(ms, imgOptions);
        ms.Position = 0;
        options.Stream = ms;

        // Set a custom base URL for the generated images (optional)
        options.CustomPath = "thumbnails/";
    }

    // Called after the HTML renderer finishes reading the stream
    public void CloseStream(StreamProviderOptions options)
    {
        options.Stream?.Dispose();
        options.Stream = null;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Path for the generated HTML file
            string htmlPath = "output.html";

            // Load the diagram
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.StreamProvider = new ThumbnailStreamProvider(diagram);
                htmlOptions.ExportHiddenPage = false; // optional: skip hidden pages

                // Export to HTML; thumbnails will be embedded via the custom stream provider
                diagram.Save(htmlPath, htmlOptions);
            }

            Console.WriteLine("HTML export with thumbnails completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}