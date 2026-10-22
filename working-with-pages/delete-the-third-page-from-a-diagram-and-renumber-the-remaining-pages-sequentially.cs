using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
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
                // Delete the third page (index 2) if it exists.
                if (diagram.Pages.Count >= 3)
                {
                    // Retrieve the page to remove.
                    Page pageToRemove = diagram.Pages[2];
                    // Remove the page from the collection.
                    diagram.Pages.Remove(pageToRemove);
                    Console.WriteLine("Third page removed successfully.");
                }
                else
                {
                    Console.WriteLine("Diagram contains fewer than three pages; no page removed.");
                }

                // Renumber remaining pages sequentially starting from 1.
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Access each page explicitly.
                    Page page = diagram.Pages[i];
                    // Set the page ID to a sequential value.
                    page.ID = i + 1;
                }
                Console.WriteLine("Pages renumbered sequentially.");

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Write any errors that occur during processing.
                Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            }
        }
    }
}