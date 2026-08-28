using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define input file path
        string inputPath = "input.vsdx"; // TODO: replace with actual file path
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output.vsdx"; // TODO: replace with desired output path

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the page by its textual name
            string pageName = "MyPage"; // TODO: replace with the target page name
            Page page = diagram.Pages.GetPage(pageName);
            if (page == null)
            {
                Console.Error.WriteLine($"Page \"{pageName}\" not found.");
                return;
            }

            // Hide the page from the UI by setting UIVisibility to Hidden
            page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;

            // Save the modified diagram to the output path using VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}