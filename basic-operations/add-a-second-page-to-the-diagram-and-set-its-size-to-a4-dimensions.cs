using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (provide via command‑line arguments or use defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source diagram exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Determine the highest existing page ID to assign a unique ID to the new page
            int maxPageId = 0;
            foreach (Page existingPage in diagram.Pages)
            {
                if (existingPage.ID > maxPageId)
                    maxPageId = existingPage.ID;
            }

            // Create a new blank page
            Page newPage = new Page();
            newPage.ID = maxPageId + 1;
            newPage.Name = $"Page {newPage.ID}";

            // Set the page size to A4 dimensions (8.27 x 11.69 inches)
            // Page dimensions are stored in inches as double values
            newPage.PageSheet.PageProps.PageWidth.Value = 8.27;   // Width in inches
            newPage.PageSheet.PageProps.PageHeight.Value = 11.69; // Height in inches

            // Add the new page to the diagram
            diagram.Pages.Add(newPage);

            // Save the modified diagram to the specified output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}