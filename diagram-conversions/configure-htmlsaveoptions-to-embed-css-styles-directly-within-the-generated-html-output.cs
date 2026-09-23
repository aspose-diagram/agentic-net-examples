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
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
            // By default Aspose.Diagram embeds CSS; no explicit property is required.
            htmlOptions.ExportHiddenPage = false; // Do not export hidden pages

            // Save the diagram as an HTML file using the configured options
            diagram.Save("output.html", htmlOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error during HTML export: {ex.Message}");
        }
    }
}