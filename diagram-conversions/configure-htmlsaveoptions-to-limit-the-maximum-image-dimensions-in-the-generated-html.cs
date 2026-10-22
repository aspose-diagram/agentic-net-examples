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
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure HTML save options
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Do not export hidden pages
            htmlOptions.ExportHiddenPage = false;

            // NOTE: HTMLSaveOptions does not provide direct properties to limit image dimensions.
            // To control image size, post‑process the generated HTML or implement a custom
            // IStreamProvider that resizes images before they are written. This example
            // proceeds without additional image size configuration.

            // Save the diagram as HTML using the configured options
            diagram.Save(outputPath, htmlOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
