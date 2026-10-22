using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio diagram file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(diagramPath);

            // Initialize the page size cache for the loaded diagram.
            PageSizeCache cache = new PageSizeCache(diagram);

            // Example usage: print cached page sizes.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                PageSize size = cache.GetPageSize(i);
                Console.WriteLine($"Page {i + 1}: Width = {size.Width} inches, Height = {size.Height} inches");
            }

            // Example usage: modify each page size using the cached values (increase by 10%).
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                PageSize originalSize = cache.GetPageSize(i);
                double newWidth = originalSize.Width * 1.1;
                double newHeight = originalSize.Height * 1.1;

                Page page = diagram.Pages[i];
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;
            }

            // Save the modified diagram to a new file in VSDX format.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                "Modified_" + Path.GetFileName(diagramPath));

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}

// Helper class that caches page width and height values to avoid repeated property accesses.
class PageSizeCache
{
    // Internal dictionary: key = page index, value = cached size.
    private readonly Dictionary<int, PageSize> _cache = new Dictionary<int, PageSize>();

    // Constructor populates the cache from the provided diagram.
    public PageSizeCache(Diagram diagram)
    {
        if (diagram == null) throw new ArgumentNullException(nameof(diagram));

        // Iterate through all pages and store their dimensions.
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            Page page = diagram.Pages[i];
            double width = page.PageSheet.PageProps.PageWidth.Value;
            double height = page.PageSheet.PageProps.PageHeight.Value;
            _cache[i] = new PageSize(width, height);
        }
    }

    // Retrieves the cached size for the specified page index.
    public PageSize GetPageSize(int pageIndex)
    {
        if (_cache.TryGetValue(pageIndex, out PageSize size))
        {
            return size;
        }
        else
        {
            // This situation should not occur; handle gracefully.
            throw new ArgumentOutOfRangeException(nameof(pageIndex), $"Page index {pageIndex} is out of range.");
        }
    }
}

// Simple immutable struct to hold page width and height.
struct PageSize
{
    public double Width { get; }
    public double Height { get; }

    public PageSize(double width, double height)
    {
        Width = width;
        Height = height;
    }
}