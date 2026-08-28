using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the first hidden page (UIVisibility == Hidden)
                Page hiddenPage = null;
                foreach (Page page in diagram.Pages)
                {
                    // UIVisibility.Value is of type UIVisibilityValue enum; compare with Hidden
                    if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
                    {
                        hiddenPage = page;
                        break;
                    }
                }

                if (hiddenPage == null)
                {
                    throw new Exception("No hidden page found in the diagram.");
                }

                // Move the hidden page to the end of the page collection
                int lastIndex = diagram.Pages.Count - 1;
                hiddenPage.MoveTo(lastIndex);

                // Verify that UIVisibility is still Hidden after moving
                if (hiddenPage.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Hidden)
                {
                    throw new Exception("UIVisibility changed after moving the hidden page.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Hidden page moved to the end and UIVisibility verified as hidden.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}