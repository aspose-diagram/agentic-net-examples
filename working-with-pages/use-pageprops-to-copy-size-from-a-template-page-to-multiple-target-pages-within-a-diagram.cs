using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least three arguments: diagramPath, templatePageName, targetPageName(s)
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <diagramPath> <templatePageName> <targetPageName1> [<targetPageName2> ...]");
            return;
        }

        string diagramPath = args[0];
        string templatePageName = args[1];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the template page by name
            Page templatePage = diagram.Pages.GetPage(templatePageName);
            if (templatePage == null)
            {
                Console.Error.WriteLine($"Error: Template page '{templatePageName}' not found.");
                return;
            }

            // Read width and height from the template page's PageProps
            double templateWidth = templatePage.PageSheet.PageProps.PageWidth.Value;
            double templateHeight = templatePage.PageSheet.PageProps.PageHeight.Value;

            // Iterate over each target page name provided in the arguments
            for (int i = 2; i < args.Length; i++)
            {
                string targetPageName = args[i];
                Page targetPage = diagram.Pages.GetPage(targetPageName);
                if (targetPage == null)
                {
                    Console.Error.WriteLine($"Warning: Target page '{targetPageName}' not found. Skipping.");
                    continue;
                }

                // Copy size from the template page to the target page
                targetPage.PageSheet.PageProps.PageWidth.Value = templateWidth;
                targetPage.PageSheet.PageProps.PageHeight.Value = templateHeight;

                Console.WriteLine($"Copied size to page '{targetPageName}'.");
            }

            // Prepare output path (original name with "_updated" suffix)
            string directory = Path.GetDirectoryName(diagramPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(diagramPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_updated.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}