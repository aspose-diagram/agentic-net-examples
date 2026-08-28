using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the modified Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Identify background pages (Background == BOOL.True)
                if (page.Background == BOOL.True)
                {
                    // Hide the page from the UI and from export by setting UIVisibility to Hidden
                    page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                }
            }

            // Save the updated diagram; UIVisibility will keep hidden pages from being shown or exported
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Release diagram resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}