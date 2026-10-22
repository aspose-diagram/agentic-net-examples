using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
                // Locate the first page whose UIVisibility is not Visible (i.e., hidden)
                Page hiddenPage = null;
                int hiddenPageIndex = -1;

                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];
                    // UIVisibility.Value is of type UIVisibilityValue; compare against Visible
                    if (page.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Visible)
                    {
                        hiddenPage = page;
                        hiddenPageIndex = i;
                        break;
                    }
                }

                if (hiddenPage == null)
                {
                    Console.Error.WriteLine("No hidden page was found in the diagram.");
                    return;
                }

                // Move the hidden page to the end of the collection if it is not already last
                int targetIndex = diagram.Pages.Count - 1;
                if (hiddenPageIndex != targetIndex)
                {
                    hiddenPage.MoveTo(targetIndex);
                }

                // Verify that UIVisibility is still not Visible after moving
                if (hiddenPage.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Visible)
                {
                    throw new Exception("UIVisibility of the moved page is Visible after relocation.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}