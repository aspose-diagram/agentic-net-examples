using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Returns true if any page in the diagram is hidden.
    static bool AnyPageHidden(string filePath)
    {
        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // UIVisibility.Value returns a UIVisibilityValue enum.
                // Any value other than Visible indicates the page is hidden.
                if (page.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Visible)
                    return true;
            }
        }
        catch (Exception ex)
        {
            // Write any loading or processing errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }

        // No hidden pages found (or an error occurred).
        return false;
    }

    static void Main(string[] args)
    {
        // Path to the Visio diagram file.
        string diagramPath = "sample.vsdx";

        // Guard to ensure the file exists before attempting to load it.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Determine whether the diagram contains any hidden pages.
        bool hasHiddenPage = AnyPageHidden(diagramPath);

        // Output the result to the console.
        Console.WriteLine(hasHiddenPage
            ? "At least one page is hidden."
            : "No hidden pages detected.");
    }
}