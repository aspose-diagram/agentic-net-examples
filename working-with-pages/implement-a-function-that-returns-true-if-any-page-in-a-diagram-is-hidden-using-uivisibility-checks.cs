using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the file exists before attempting to load.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(filePath))
            {
                // Determine if any page is hidden.
                bool hasHiddenPage = AnyPageHidden(diagram);

                // Output the result.
                Console.WriteLine($"Any hidden page: {hasHiddenPage}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks all pages in the given diagram and returns true if any page is hidden.
    /// A page is considered hidden when its UIVisibility cell value equals UIVisibilityValue.Hidden.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <returns>True if at least one page is hidden; otherwise, false.</returns>
    static bool AnyPageHidden(Diagram diagram)
    {
        // Iterate over each page using an index to avoid var in foreach (per rules).
        for (int i = 0; i < diagram.Pages.Count; i++)
        {
            Page page = diagram.Pages[i];

            // UIVisibility.Value returns a UIVisibilityValue enum.
            if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
            {
                return true;
            }
        }

        // No hidden pages found.
        return false;
    }
}