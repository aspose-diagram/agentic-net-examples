using System;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramPageSizeCacheExample
{
    // Simple cache for page dimensions (width and height in inches)
    public static class PageSizeCache
    {
        // Key: page ID, Value: tuple of width and height
        private static readonly Dictionary<int, (double Width, double Height)> _cache = new();

        // Retrieves the cached size or reads from the page and stores it
        public static (double Width, double Height) GetPageSize(Page page)
        {
            if (page == null) throw new ArgumentNullException(nameof(page));

            int pageId = page.ID;
            if (_cache.TryGetValue(pageId, out var size))
            {
                return size;
            }

            // Access the page size properties (values are in inches)
            double width = page.PageSheet.PageProps.PageWidth.Value;
            double height = page.PageSheet.PageProps.PageHeight.Value;

            // Store in cache for future calls
            _cache[pageId] = (width, height);
            return (width, height);
        }

        // Optional: clear the cache (e.g., when pages are added/removed)
        public static void Clear() => _cache.Clear();
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Load a Visio diagram (replace with your file path)
                string diagramPath = "input.vsdx";
                using var diagram = new Diagram(diagramPath);

                // Iterate through all pages and use the cache to obtain sizes
                foreach (Page page in diagram.Pages)
                {
                    var (width, height) = PageSizeCache.GetPageSize(page);
                    Console.WriteLine($"Page ID {page.ID}: Width = {width} in, Height = {height} in");

                    // Example: modify page size only if needed
                    // (Here we just demonstrate reading; uncomment to set new size)
                    // if (width != 8.5 || height != 11)
                    // {
                    //     page.PageSheet.PageProps.PageWidth.Value = 8.5;
                    //     page.PageSheet.PageProps.PageHeight.Value = 11;
                    //     // Update cache to reflect the change
                    //     PageSizeCache.Clear(); // simple approach: clear all
                    // }
                }

                // Save the diagram after any modifications (if any)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}