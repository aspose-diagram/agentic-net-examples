using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class PageSizeCache
{
    // Cache keyed by page ID (int) storing width and height in inches
    private readonly Dictionary<int, (double Width, double Height)> _cache = new();

    // Try to get cached size; returns false if not cached
    public bool TryGetSize(int pageId, out double width, out double height)
    {
        if (_cache.TryGetValue(pageId, out var size))
        {
            width = size.Width;
            height = size.Height;
            return true;
        }

        width = height = 0;
        return false;
    }

    // Store size in cache
    public void SetSize(int pageId, double width, double height)
    {
        _cache[pageId] = (width, height);
    }
}

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            const string inputPath = "input.vsdx";
            // Path for the output Visio file
            const string outputPath = "output.vsdx";

            // Load the diagram
            using Diagram diagram = new Diagram(inputPath);

            // Initialize the cache
            PageSizeCache sizeCache = new PageSizeCache();

            // Iterate through all pages and cache their sizes
            foreach (Page page in diagram.Pages)
            {
                int pageId = page.ID;

                // Retrieve width and height from the page sheet
                double width = page.PageSheet.PageProps.PageWidth.Value;
                double height = page.PageSheet.PageProps.PageHeight.Value;

                // Store in cache
                sizeCache.SetSize(pageId, width, height);
            }

            // Example usage: read cached size and reapply (no actual change, just demonstration)
            foreach (Page page in diagram.Pages)
            {
                int pageId = page.ID;

                if (sizeCache.TryGetSize(pageId, out double cachedWidth, out double cachedHeight))
                {
                    // Reassign the same values to demonstrate cache usage
                    page.PageSheet.PageProps.PageWidth.Value = cachedWidth;
                    page.PageSheet.PageProps.PageHeight.Value = cachedHeight;
                }
                else
                {
                    // This branch should not occur because we cached all pages earlier
                    Console.WriteLine($"Page ID {pageId} size not found in cache.");
                }
            }

            // Save the diagram back to a Visio file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}