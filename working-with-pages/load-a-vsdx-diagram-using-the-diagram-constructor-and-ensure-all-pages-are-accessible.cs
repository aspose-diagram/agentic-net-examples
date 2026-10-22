using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input‑vsdx‑file>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the VSDX diagram using the Diagram constructor.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the total number of pages.
            int pageCount = diagram.Pages.Count;
            Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");

            // Iterate through all pages to ensure they are accessible.
            // Explicitly type the iterator as Aspose.Diagram.Page.
            for (int i = 0; i < pageCount; i++)
            {
                Aspose.Diagram.Page page = diagram.Pages[i];
                // Output basic page information.
                Console.WriteLine($"Page {i + 1}: ID = {page.ID}, Name = {page.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}