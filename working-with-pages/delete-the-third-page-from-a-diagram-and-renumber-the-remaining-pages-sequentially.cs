using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Ensure the diagram has at least three pages.
                if (diagram.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The diagram contains fewer than three pages; nothing to delete.");
                    return;
                }

                // Retrieve the third page (zero‑based index 2) and remove it.
                Page pageToRemove = diagram.Pages[2];
                diagram.Pages.Remove(pageToRemove);

                // Renumber remaining pages sequentially starting from 1.
                int newId = 1;
                foreach (Page page in diagram.Pages)
                {
                    page.ID = newId++;
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Third page removed and diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}