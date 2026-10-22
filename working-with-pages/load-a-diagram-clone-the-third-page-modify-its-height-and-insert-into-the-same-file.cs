using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as an argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram has at least three pages.
            if (diagram.Pages.Count < 3)
            {
                Console.Error.WriteLine("The diagram does not contain a third page to clone.");
                return;
            }

            // Retrieve the third page (zero‑based index).
            Page sourcePage = diagram.Pages[2];

            // Create a new blank page instance.
            Page clonedPage = new Page();

            // Copy the contents of the source page into the new page.
            clonedPage.Copy(sourcePage);

            // Assign a unique ID to the cloned page (max existing ID + 1).
            int maxId = int.MinValue;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId)
                    maxId = p.ID;
            }
            clonedPage.ID = maxId + 1;

            // Optionally give the cloned page a distinct name.
            clonedPage.Name = sourcePage.Name + "_Clone";

            // Modify the height of the cloned page.
            // Example: increase the height by 1.0 inch (or any desired value).
            double originalHeight = clonedPage.PageSheet.PageProps.PageHeight.Value;
            clonedPage.PageSheet.PageProps.PageHeight.Value = originalHeight + 1.0;

            // Insert the cloned page into the diagram's pages collection.
            diagram.Pages.Add(clonedPage);

            // Save the updated diagram back to the same file (overwrites original).
            diagram.Save(inputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}