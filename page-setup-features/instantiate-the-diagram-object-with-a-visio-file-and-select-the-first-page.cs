using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file; can be provided as a command‑line argument or defaulted
        string filePath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(filePath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Select the first page (index 0) from the diagram
                Page firstPage = diagram.Pages[0];

                // Output basic information about the selected page
                Console.WriteLine($"First page selected: Name = {firstPage.Name}, ID = {firstPage.ID}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}