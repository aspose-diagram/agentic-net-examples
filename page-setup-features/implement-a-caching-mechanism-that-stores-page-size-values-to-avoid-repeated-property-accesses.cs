using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Initialize the page size cache.
            PageSizeCache cache = new PageSizeCache();

            // Iterate through all pages and retrieve cached sizes.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                (double width, double height) = cache.GetPageSize(page);
                Console.WriteLine($"Page {i + 1}: Width = {width} inches, Height = {height} inches");
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

/// <summary>
/// Caches page width and height values to avoid repeated property accesses.
/// </summary>
class PageSizeCache
{
    // Internal dictionary keyed by page ID.
    private readonly Dictionary<int, (double width, double height)> _cache = new Dictionary<int, (double, double)>();

    /// <summary>
    /// Retrieves the size of the specified page, using the cache when possible.
    /// </summary>
    /// <param name="page">The page whose size is required.</param>
    /// <returns>A tuple containing width and height in inches.</returns>
    public (double width, double height) GetPageSize(Page page)
    {
        // Use the page's ID as the cache key (IDs are unique per page).
        int key = page.ID;

        // Return cached value if it exists.
        if (_cache.TryGetValue(key, out var size))
        {
            return size;
        }

        // Access the page properties to obtain width and height.
        double width = page.PageSheet.PageProps.PageWidth.Value;
        double height = page.PageSheet.PageProps.PageHeight.Value;

        // Store the retrieved size in the cache for future use.
        _cache[key] = (width, height);

        return (width, height);
    }
}