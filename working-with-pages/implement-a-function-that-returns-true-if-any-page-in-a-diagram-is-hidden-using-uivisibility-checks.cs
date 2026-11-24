using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect a single argument: path to the Visio file
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool hidden = AnyPageHidden(diagram);
                Console.WriteLine($"Any hidden page: {hidden}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or IO exceptions and report them
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks all pages in the provided diagram and returns true if any page is hidden.
    /// A page is considered hidden when its UIVisibility cell value equals UIVisibilityValue.Hidden.
    /// </summary>
    /// <param name="diagram">The loaded Aspose.Diagram.Diagram instance.</param>
    /// <returns>True if at least one page is hidden; otherwise, false.</returns>
    static bool AnyPageHidden(Diagram diagram)
    {
        // Iterate over each page using explicit type to avoid var issues
        foreach (Page page in diagram.Pages)
        {
            // UIVisibility.Value indicates hidden state (UIVisibilityValue.Hidden means hidden)
            if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
            {
                return true;
            }
        }

        // No hidden pages found
        return false;
    }
}