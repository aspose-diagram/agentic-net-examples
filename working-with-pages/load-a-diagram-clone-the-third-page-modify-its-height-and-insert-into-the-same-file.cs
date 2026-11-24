using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least three pages.
            if (diagram.Pages.Count < 3)
            {
                Console.Error.WriteLine("Error: Diagram does not contain at least three pages.");
                return;
            }

            // Retrieve the third page (zero‑based index 2).
            Page sourcePage = diagram.Pages[2];

            // Determine a new unique page ID.
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new page instance for the clone.
            Page clonedPage = new Page();
            clonedPage.ID = maxPageId + 1;
            clonedPage.Name = sourcePage.Name + "_Clone";

            // Copy the contents of the source page into the new page.
            clonedPage.Copy(sourcePage);

            // Modify the height of the cloned page (increase by 2.0 units).
            double originalHeight = clonedPage.PageSheet.PageProps.PageHeight.Value;
            clonedPage.PageSheet.PageProps.PageHeight.Value = originalHeight + 2.0;

            // Insert the cloned page into the diagram.
            diagram.Pages.Add(clonedPage);

            // Save the updated diagram back to the original file.
            diagram.Save(inputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Page cloned, height modified, and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}