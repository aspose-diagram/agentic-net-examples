using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (optional second argument)
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(inputPath) + "_modified.vsdx");

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the first page whose UIVisibility is hidden
                Page hiddenPage = null;
                foreach (Page page in diagram.Pages)
                {
                    // UIVisibility.Value is of type UIVisibilityValue
                    if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
                    {
                        hiddenPage = page;
                        break;
                    }
                }

                if (hiddenPage == null)
                {
                    Console.Error.WriteLine("No hidden page (UIVisibility == Hidden) found in the diagram.");
                    return;
                }

                // Move the hidden page to the end of the page collection
                int lastIndex = diagram.Pages.Count - 1;
                hiddenPage.MoveTo(lastIndex);

                // Verify that UIVisibility is still hidden after moving
                if (hiddenPage.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Hidden)
                {
                    throw new Exception("UIVisibility changed after moving the page. Expected UIVisibilityValue.Hidden.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}