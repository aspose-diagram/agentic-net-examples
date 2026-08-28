using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramPageSizeCaching
{
    // Simple cache for page dimensions (width and height in inches)
    public static class PageSizeCache
    {
        // Key: Page ID (unique within a diagram), Value: tuple of width and height
        private static readonly Dictionary<long, (double Width, double Height)> _cache = new();

        // Retrieves the size of the specified page, using the cache when possible.
        public static (double Width, double Height) GetPageSize(Diagram diagram, int pageIndex)
        {
            // Validate page index
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "Invalid page index.");

            // Get the page object
            Page page = diagram.Pages[pageIndex];

            // Use the page's unique ID as the cache key
            long pageId = page.ID;

            // Return cached value if it exists
            if (_cache.TryGetValue(pageId, out var size))
                return size;

            // Access the page properties (this is the expensive operation we want to avoid repeating)
            double width = page.PageSheet.PageProps.PageWidth.Value;
            double height = page.PageSheet.PageProps.PageHeight.Value;

            // Store in cache for future calls
            _cache[pageId] = (width, height);

            return (width, height);
        }

        // Clears the cache (optional utility)
        public static void Clear()
        {
            _cache.Clear();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file (replace with an actual file path)
                const string diagramPath = "example.vsdx";

                // Load the diagram
                using Diagram diagram = new Diagram(diagramPath);

                // Example: retrieve sizes for all pages using the cache
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    var (width, height) = PageSizeCache.GetPageSize(diagram, i);
                    Console.WriteLine($"Page {i + 1} (ID={diagram.Pages[i].ID}): Width={width} inches, Height={height} inches");
                }

                // Optional: demonstrate that repeated calls hit the cache (no additional property access)
                // The following call will use the cached values for page 0
                var cachedSize = PageSizeCache.GetPageSize(diagram, 0);
                Console.WriteLine($"Cached size for first page: Width={cachedSize.Width}, Height={cachedSize.Height}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}