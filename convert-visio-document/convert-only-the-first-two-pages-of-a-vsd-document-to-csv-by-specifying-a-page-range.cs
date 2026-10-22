using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input VSD file path and output CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsd> <output.csv>");
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
            // Load the source Visio diagram
            Diagram sourceDiagram = new Diagram(inputPath);

            // Create a new empty diagram to hold only the required pages
            Diagram targetDiagram = new Diagram();

            // Copy all masters from source to target (required for page content)
            foreach (Master master in sourceDiagram.Masters)
            {
                targetDiagram.Masters.Add(master);
            }

            // Determine the current maximum page ID in the target diagram
            int maxPageId = 0;
            foreach (Page p in targetDiagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Add the first two pages (if they exist) from the source diagram
            int pagesToCopy = Math.Min(2, sourceDiagram.Pages.Count);
            for (int i = 0; i < pagesToCopy; i++)
            {
                Page srcPage = sourceDiagram.Pages[i];

                // Assign a new unique ID for the page in the target diagram
                srcPage.ID = ++maxPageId;

                // Add the page to the target diagram
                targetDiagram.Pages.Add(srcPage);
            }

            // Remove the default empty page that was created with the new diagram
            if (targetDiagram.Pages.Count > pagesToCopy)
            {
                // The first page (index 0) is the original empty page
                targetDiagram.Pages.Remove(targetDiagram.Pages[0]);
            }

            // Save the target diagram as CSV (contains only the first two pages)
            targetDiagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine($"Successfully exported first {pagesToCopy} page(s) to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}