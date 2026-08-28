using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: source diagram path, destination diagram path, output diagram path
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <source.vsdx> <destination.vsdx> <output.vsdx>");
            return;
        }

        string sourcePath = args[0];
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        string destPath = args[1];
        if (!File.Exists(destPath))
        {
            Console.Error.WriteLine($"File not found: {destPath}");
            return;
        }

        string outputPath = args[2];
        // No need to check output existence; it will be created/overwritten.

        try
        {
            // Load the source diagram containing the page to copy
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Load the destination diagram that will receive the copied page
            Diagram destDiagram = new Diagram(destPath);

            // Retrieve the first page from the source diagram (adjust as needed)
            Page sourcePage = sourceDiagram.Pages[0];

            // Create a new page instance for the destination diagram
            Page newPage = new Page();

            // Copy the content of the source page into the new page
            newPage.Copy(sourcePage);

            // Set the new page as a background page
            newPage.Background = BOOL.True;

            // Ensure the new page has a unique ID within the destination diagram
            int maxId = 0;
            foreach (Page p in destDiagram.Pages)
            {
                if (p.ID > maxId) maxId = p.ID;
            }
            newPage.ID = maxId + 1;

            // Add the new background page to the destination diagram
            destDiagram.Pages.Add(newPage);

            // Save the modified destination diagram to the specified output file
            destDiagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}