using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Identify background pages (pages marked as background)
                    if (page.Background == BOOL.True)
                    {
                        // Hide the background page by setting UIVisibility to Hidden
                        page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                    }
                }

                // Save the modified diagram with the same format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Background pages have been hidden and the diagram saved.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}