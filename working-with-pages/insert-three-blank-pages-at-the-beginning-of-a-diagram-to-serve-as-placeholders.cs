using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the current maximum Page ID to assign unique IDs to new pages
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Insert three blank pages at the beginning of the diagram
            for (int i = 0; i < 3; i++)
            {
                // Create a new blank page and assign a unique ID and name
                Page newPage = new Page();
                newPage.Name = $"BlankPage{i + 1}";
                newPage.ID = ++maxPageId;

                // Add the new page to the diagram (adds at the end)
                diagram.Pages.Add(newPage);

                // Retrieve the page we just added (it is the last page in the collection)
                Page addedPage = diagram.Pages[diagram.Pages.Count - 1];

                // Move the newly added page to the start (index 0)
                addedPage.MoveTo(0);
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}