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
            Console.Error.WriteLine("Usage: Program <diagramPath> <pageName>");
            return;
        }

        string diagramPath = args[0];
        string targetPageName = args[1];

        // Guard against missing input file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the page by its name.
            Page page = diagram.Pages.GetPage(targetPageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Page with name '{targetPageName}' not found.");
                return;
            }

            // Output basic information about the page.
            Console.WriteLine($"Page '{targetPageName}' found. It contains {page.Shapes.Count} shape(s).");

            // Iterate through all shapes on the retrieved page.
            for (int i = 0; i < page.Shapes.Count; i++)
            {
                Shape shape = page.Shapes[i];

                // Display shape details: ID, universal name, and master name (if any).
                string masterName = shape.Master != null ? shape.Master.Name : "None";
                Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}, Master: {masterName}");
            }

            // Save the diagram unchanged to a new file (optional demonstration).
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_out.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}