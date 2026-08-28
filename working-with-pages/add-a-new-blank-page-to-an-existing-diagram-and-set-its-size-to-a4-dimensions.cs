using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the existing Visio diagram (modify as needed)
        string inputPath = "input.vsdx";
        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the updated diagram with the new page
        string outputPath = "output.vsdx";

        try
        {
            // Load the existing diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Determine the highest existing page ID to assign a unique ID to the new page
            int maxPageId = 0;
            foreach (Page page in diagram.Pages)
            {
                if (page.ID > maxPageId)
                    maxPageId = page.ID;
            }

            // Create a new blank page with an ID one greater than the current maximum
            Page newPage = new Page(maxPageId + 1);
            // Assign a friendly name to the new page (optional)
            newPage.Name = "BlankPage";

            // Set the page dimensions to A4 size (width = 8.27 inches, height = 11.69 inches)
            newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
            newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

            // Add the newly configured page to the diagram's page collection
            diagram.Pages.Add(newPage);

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}