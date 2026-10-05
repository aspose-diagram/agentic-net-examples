using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input diagram path (first argument) or default placeholder
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output diagram path (second argument) or default placeholder
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new page instance
            Page newPage = new Page();

            // Determine the next available page ID
            int maxId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxId)
                    maxId = p.ID;
            }
            newPage.ID = maxId + 1;
            newPage.Name = "Page2";

            // Set the page size to A4 dimensions (in inches)
            // A4 width = 8.27 inches, height = 11.69 inches
            newPage.PageSheet.PageProps.PageWidth.Value = 8.27;
            newPage.PageSheet.PageProps.PageHeight.Value = 11.69;

            // Add the new page to the diagram
            diagram.Pages.Add(newPage);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}