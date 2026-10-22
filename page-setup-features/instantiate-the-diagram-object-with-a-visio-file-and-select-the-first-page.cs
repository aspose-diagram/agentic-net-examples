using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file. Use the first command‑line argument if provided, otherwise a default name.
        string filePath = args.Length > 0 ? args[0] : "sample.vsdx";

        // Guard: ensure the file exists before attempting to load it.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified Visio file.
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Select the first page (index 0) from the diagram.
            Page firstPage = diagram.Pages[0];

            // Output basic information about the selected page.
            Console.WriteLine($"First page name: {firstPage.Name}");
            Console.WriteLine($"First page ID: {firstPage.ID}");

            // Dispose the diagram to release resources.
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}