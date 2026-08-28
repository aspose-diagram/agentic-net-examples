using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Desired output XPS file path
            string outputPath = "output.xps";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure XPS save options to exclude hidden pages
            XPSSaveOptions options = new XPSSaveOptions();
            options.ExportHiddenPage = false;

            // Save the diagram as XPS
            diagram.Save(outputPath, options);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("Diagram successfully saved to XPS without hidden pages.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
