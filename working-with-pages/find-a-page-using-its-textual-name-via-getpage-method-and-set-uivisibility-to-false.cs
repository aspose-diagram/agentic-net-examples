using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Name of the page to modify
            string targetPageName = "MyPage";

            // Retrieve the page by its textual name
            Page page = diagram.Pages.GetPage(targetPageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Page \"{targetPageName}\" not found.");
                diagram.Dispose();
                return;
            }

            // Hide the page from the UI by setting UIVisibility to Hidden
            page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;

            // Path for the modified diagram
            string outputPath = "output.vsdx";

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Release diagram resources
            diagram.Dispose();

            Console.WriteLine("UIVisibility set to false and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}