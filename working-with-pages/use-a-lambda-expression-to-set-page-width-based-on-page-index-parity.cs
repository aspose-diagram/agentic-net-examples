using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Use a lambda expression with index to set page width based on parity
            // Even-indexed pages get width 8.5 inches, odd-indexed pages get width 11 inches
            diagram.Pages
                .Select((page, index) =>
                {
                    double width = (index % 2 == 0) ? 8.5 : 11.0;
                    page.PageSheet.PageProps.PageWidth.Value = width;
                    return page;
                })
                .ToList(); // Force execution of the lambda for all pages

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}