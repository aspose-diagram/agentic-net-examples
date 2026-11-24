using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output HTML file path
        string outputPath = "output.html";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Remove hidden shapes, masters, and styles before exporting
                diagram.RemoveHiddenInformation(
                    (int)(RemoveHiddenInfoItem.Shapes |
                          RemoveHiddenInfoItem.Masters |
                          RemoveHiddenInfoItem.Styles));

                // Create HTML save options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

                // Exclude hidden pages from the HTML export
                htmlOptions.ExportHiddenPage = false; // bool, not BOOL

                // Save the diagram to HTML using the configured options
                diagram.Save(outputPath, htmlOptions);
            }

            Console.WriteLine($"Diagram successfully saved to HTML: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
