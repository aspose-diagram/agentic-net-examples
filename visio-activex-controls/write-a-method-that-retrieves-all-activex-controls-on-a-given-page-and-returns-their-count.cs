using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageIndex>");
            return;
        }

        string diagramPath = args[0];
        string pageIndexArg = args[1];

        // Guard: check if the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        // Parse page index.
        if (!int.TryParse(pageIndexArg, out int pageIndex))
        {
            Console.Error.WriteLine("Error: Page index must be an integer.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Retrieve the count of ActiveX controls on the specified page.
                int count = GetActiveXControlCount(diagram, pageIndex);
                Console.WriteLine($"ActiveX control count on page {pageIndex}: {count}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves all ActiveX controls on the given page and returns their count.
    /// </summary>
    /// <param name="diagram">The loaded Diagram instance.</param>
    /// <param name="pageIndex">Zero‑based index of the page to inspect.</param>
    /// <returns>Number of shapes that contain an ActiveX control.</returns>
    static int GetActiveXControlCount(Diagram diagram, int pageIndex)
    {
        // Validate page index range.
        if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
        {
            Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range.");
            return 0;
        }

        // Access the requested page.
        Page page = diagram.Pages[pageIndex];

        int activeXCount = 0;

        // Iterate through all shapes on the page.
        foreach (Shape shape in page.Shapes)
        {
            // Skip logically deleted shapes.
            if (shape.Del == BOOL.True)
                continue;

            // If the shape hosts an ActiveX control, increment the counter.
            if (shape.ActiveXControl != null)
                activeXCount++;
        }

        return activeXCount;
    }
}