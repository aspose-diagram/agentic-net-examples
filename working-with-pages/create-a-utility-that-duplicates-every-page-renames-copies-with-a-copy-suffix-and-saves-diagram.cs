using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect original pages to avoid modifying the collection during iteration.
                List<Page> originalPages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    originalPages.Add(p);
                }

                // Determine the current maximum page ID.
                int maxPageId = 0;
                foreach (Page p in diagram.Pages)
                {
                    if (p.ID > maxPageId)
                        maxPageId = p.ID;
                }

                // Duplicate each page.
                foreach (Page originalPage in originalPages)
                {
                    // Increment ID for the new page.
                    maxPageId++;

                    // Create a new blank page.
                    Page copiedPage = new Page();

                    // Assign a unique ID.
                    copiedPage.ID = maxPageId;

                    // Copy the contents of the original page into the new page.
                    copiedPage.Copy(originalPage);

                    // Rename the copied page with a "_Copy" suffix.
                    copiedPage.Name = originalPage.Name + "_Copy";

                    // Add the copied page to the diagram.
                    diagram.Pages.Add(copiedPage);
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}