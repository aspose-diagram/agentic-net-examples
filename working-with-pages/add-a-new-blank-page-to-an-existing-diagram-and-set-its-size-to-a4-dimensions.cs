using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID (as int) to assign a unique ID to the new page
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    // Page.ID is a long; cast to int for comparison
                    int pageId = (int)existingPage.ID;
                    if (pageId > maxPageId)
                        maxPageId = pageId;
                }

                // Create a new blank page and set its ID and name
                Page newPage = new Page();
                newPage.ID = maxPageId + 1;
                newPage.Name = $"Page-{newPage.ID}";

                // Set the page size to A4 dimensions (in inches)
                // Width = 8.27 inches, Height = 11.69 inches
                newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
                newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

                // Add the new page to the diagram
                diagram.Pages.Add(newPage);

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}