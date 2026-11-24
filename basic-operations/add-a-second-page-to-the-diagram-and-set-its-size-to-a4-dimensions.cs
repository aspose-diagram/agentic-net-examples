using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Determine the maximum existing page ID to assign a unique ID to the new page.
                int maxPageId = 0;
                foreach (Page existingPage in diagram.Pages)
                {
                    if (existingPage.ID > maxPageId)
                        maxPageId = existingPage.ID;
                }

                // Create a new page instance.
                Page newPage = new Page();

                // Assign a unique ID and a name to the new page.
                newPage.ID = maxPageId + 1;
                newPage.Name = "Page2";

                // Set the page size to A4 dimensions (8.27 x 11.69 inches).
                newPage.PageSheet.PageProps.PageWidth.Value = 8.27;   // Width in inches
                newPage.PageSheet.PageProps.PageHeight.Value = 11.69; // Height in inches

                // Add the newly created page to the diagram.
                diagram.Pages.Add(newPage);

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}