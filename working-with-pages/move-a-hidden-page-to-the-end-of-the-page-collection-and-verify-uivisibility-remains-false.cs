using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the diagram from the input file
            diagram = new Diagram(inputPath);

            // Find the first hidden page (UIVisibility == Hidden)
            Page hiddenPage = null;
            foreach (Page page in diagram.Pages)
            {
                // UIVisibility is of type UIVisibilityValue, compare with UIVisibilityValue.Hidden
                if (page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden)
                {
                    hiddenPage = page;
                    break;
                }
            }

            // Throw if no hidden page was found
            if (hiddenPage == null)
            {
                throw new Exception("No hidden page with UIVisibility == Hidden was found.");
            }

            // Move the hidden page to the end of the collection
            int lastIndex = diagram.Pages.Count - 1;
            hiddenPage.MoveTo(lastIndex);

            // Verify UIVisibility is still Hidden after moving
            if (hiddenPage.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Hidden)
            {
                throw new Exception("UIVisibility changed after moving the page.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }
        finally
        {
            // Ensure resources are released
            diagram?.Dispose();
        }

        Console.WriteLine("Hidden page moved to the end and UIVisibility verified successfully.");
    }
}