using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageIndex>");
            return;
        }

        string diagramPath = args[0];
        string pageIndexArg = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine("Error: Invalid page index.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Guard: ensure the requested page exists
                if (pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the page
                Page page = diagram.Pages[pageIndex];

                // Get the count of ActiveX controls on the page
                int activeXCount = GetActiveXControlCount(page);

                Console.WriteLine($"ActiveX controls on page {pageIndex}: {activeXCount}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Counts all ActiveX controls present on the specified page.
    /// </summary>
    /// <param name="page">The page to inspect.</param>
    /// <returns>The number of shapes that contain an ActiveX control.</returns>
    static int GetActiveXControlCount(Page page)
    {
        int count = 0;

        // Iterate through all shapes on the page
        foreach (Shape shape in page.Shapes)
        {
            // Skip logically deleted shapes
            if (shape.Del == BOOL.True)
                continue;

            // If the shape hosts an ActiveX control, increment the counter
            if (shape.ActiveXControl != null)
                count++;
        }

        return count;
    }
}