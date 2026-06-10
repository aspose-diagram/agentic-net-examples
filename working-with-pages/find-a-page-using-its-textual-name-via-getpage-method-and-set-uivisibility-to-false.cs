using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Name of the page to modify
                string pageName = "MyPage";

                // Retrieve the page by its textual name
                Page page = diagram.Pages.GetPage(pageName);
                if (page == null)
                {
                    throw new Exception($"Page '{pageName}' not found.");
                }

                // Set UIVisibility to hidden (false)
                page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}