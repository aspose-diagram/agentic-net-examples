using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Find the first hidden page (UIVisibility == Hidden)
            Page hiddenPage = null;
            foreach (Page page in diagram.Pages)
            {
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
            int targetIndex = diagram.Pages.Count - 1;
            hiddenPage.MoveTo(targetIndex);

            // Verify that UIVisibility is still Hidden after moving
            if (hiddenPage.PageSheet.PageProps.UIVisibility.Value != UIVisibilityValue.Hidden)
            {
                throw new Exception("UIVisibility changed after moving the page.");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}