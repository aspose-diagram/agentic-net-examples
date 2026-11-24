using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be provided via command line arguments)
        string inputPath = args.Length > 0 ? args[0] : "template_diagram.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "updated_diagram.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page to act as the template
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Use the first page as the template for size copying
            Page templatePage = diagram.Pages[0];
            double templateWidth = templatePage.PageSheet.PageProps.PageWidth.Value;
            double templateHeight = templatePage.PageSheet.PageProps.PageHeight.Value;

            // Iterate over all pages and copy the size from the template page
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Skip the template page itself (optional; remove if you want to set it as well)
                if (page == templatePage)
                    continue;

                // Copy width and height values
                page.PageSheet.PageProps.PageWidth.Value = templateWidth;
                page.PageSheet.PageProps.PageHeight.Value = templateHeight;
            }

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}