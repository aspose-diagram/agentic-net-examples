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
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the input file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the first hidden page (UIVisibility == UIVisibilityValue.Hidden)
                Page hiddenPage = null;
                foreach (Page page in diagram.Pages)
                {
                    // UIVisibility is stored in the PageProps section as UIVisibilityValue
                    if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
                    {
                        hiddenPage = page;
                        break;
                    }
                }

                // If no hidden page is found, report and exit
                if (hiddenPage == null)
                {
                    Console.Error.WriteLine("No hidden page found in the diagram.");
                    return;
                }

                // Move the hidden page to the end of the page collection
                int targetIndex = diagram.Pages.Count - 1;
                hiddenPage.MoveTo(targetIndex);

                // Verify that UIVisibility remains hidden after moving
                if (hiddenPage.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Hidden)
                {
                    Console.Error.WriteLine("UIVisibility of the moved page is not hidden after relocation.");
                    return;
                }

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Hidden page moved to the end and UIVisibility verified successfully.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}