using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VSD file> <output CSV file>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram.
            Diagram sourceDiagram = new Diagram(inputPath);

            // Create a new empty diagram to hold only the required pages.
            Diagram targetDiagram = new Diagram();

            // The newly created diagram contains a default empty page; remove it.
            if (targetDiagram.Pages.Count > 0)
            {
                targetDiagram.Pages.Remove(targetDiagram.Pages[0]);
            }

            // Copy all masters from the source diagram to the target diagram.
            foreach (Master master in sourceDiagram.Masters)
            {
                targetDiagram.Masters.Add(master);
            }

            // Determine how many pages are available in the source diagram.
            int totalPages = sourceDiagram.Pages.Count;
            int pagesToCopy = Math.Min(2, totalPages); // we need only the first two pages.

            // Copy the first two pages (or fewer if the source has less).
            for (int i = 0; i < pagesToCopy; i++)
            {
                Page sourcePage = sourceDiagram.Pages[i];

                // Create a new page and copy the contents of the source page.
                Page newPage = new Page();
                newPage.Copy(sourcePage);

                // Add the copied page to the target diagram.
                targetDiagram.Pages.Add(newPage);
            }

            // Save the target diagram as CSV. This will include only the copied pages.
            targetDiagram.Save(outputPath, SaveFileFormat.Csv);
            Console.WriteLine($"Successfully exported first {pagesToCopy} page(s) to CSV: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}