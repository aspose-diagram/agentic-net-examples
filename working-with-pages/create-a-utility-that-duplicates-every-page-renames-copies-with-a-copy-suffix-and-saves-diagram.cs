using System;
using System.IO;
using System.Collections.Generic;
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
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Collect original pages to avoid modifying the collection while iterating
                List<Page> originalPages = new List<Page>();
                foreach (Page page in diagram.Pages)
                {
                    originalPages.Add(page);
                }

                // Determine the current maximum page ID
                int maxPageId = 0;
                foreach (Page page in diagram.Pages)
                {
                    if (page.ID > maxPageId)
                        maxPageId = page.ID;
                }

                // Duplicate each original page
                foreach (Page srcPage in originalPages)
                {
                    // Create a new page with a unique ID
                    int newPageId = ++maxPageId;
                    Page copyPage = new Page(newPageId);

                    // Copy the contents of the source page into the new page
                    copyPage.Copy(srcPage);

                    // Rename the copied page with a "_Copy" suffix
                    copyPage.Name = srcPage.Name + "_Copy";

                    // Add the copied page to the diagram
                    diagram.Pages.Add(copyPage);
                }

                // Ensure the output directory exists
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}