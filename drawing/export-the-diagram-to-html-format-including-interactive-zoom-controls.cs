using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string sourcePath = "input.vsdx";

        // Verify the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(sourcePath);

            // Configure HTML export options (zoom controls are enabled by default)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                // Export hidden pages if needed (optional)
                ExportHiddenPage = false
            };

            // Export the diagram to HTML
            string outputPath = "output.html";
            diagram.Save(outputPath, htmlOptions);

            Console.WriteLine($"Diagram exported to HTML with zoom controls at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error exporting diagram to HTML: {ex.Message}");
        }
    }
}