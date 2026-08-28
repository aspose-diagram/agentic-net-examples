using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Checks all pages in the diagram and returns true if any page is hidden.
    static bool AnyPageHidden(Diagram diagram)
    {
        // Iterate through each page in the diagram.
        foreach (Page page in diagram.Pages)
        {
            // UIVisibility cell uses UIVisibilityValue enum: Hidden indicates the page is hidden.
            if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
                return true; // Hidden page found.
        }
        return false; // No hidden pages.
    }

    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string filePath = args[0];
        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(filePath);

            // Determine if any page is hidden.
            bool hasHidden = AnyPageHidden(diagram);

            // Output the result to the console.
            Console.WriteLine(hasHidden
                ? "The diagram contains at least one hidden page."
                : "All pages in the diagram are visible.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}