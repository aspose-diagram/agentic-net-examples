using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the background page (Background == BOOL.True).
                Page backgroundPage = null;
                foreach (Page page in diagram.Pages)
                {
                    if (page.Background == BOOL.True)
                    {
                        backgroundPage = page;
                        break;
                    }
                }

                if (backgroundPage == null)
                {
                    Console.Error.WriteLine("Error: No background page found in the diagram.");
                    return;
                }

                // Hide the background page by setting its UI visibility to hidden.
                backgroundPage.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;

                // Configure save options to exclude hidden pages from export.
                XPSSaveOptions saveOptions = new XPSSaveOptions();
                saveOptions.ExportHiddenPage = false; // ExportHiddenPage expects a bool, not BOOL.

                // Save the modified diagram using the specified options.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
