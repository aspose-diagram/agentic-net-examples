using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new blank page instance
                Page newPage = new Page();

                // Determine the maximum existing page ID to assign a unique ID
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }
                newPage.ID = maxPageId + 1;
                newPage.Name = "InsertedPage";

                // Add the new page to the diagram (adds at the end)
                diagram.Pages.Add(newPage);

                // Move the newly added page to index 2 (third position, zero‑based)
                // Ensure the diagram has at least three pages after insertion
                if (diagram.Pages.Count > 2)
                {
                    newPage.MoveTo(2);
                }
                else
                {
                    Console.Error.WriteLine("Diagram does not have enough pages to move the new page to index 2.");
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}