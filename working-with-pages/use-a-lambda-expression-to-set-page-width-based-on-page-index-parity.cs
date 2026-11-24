using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Lambda expression to set page width based on page index parity
            Action<int, Page> setPageWidth = (index, page) =>
            {
                // Even index (0-based) -> width 8.5 inches, odd index -> width 11 inches
                double width = (index % 2 == 0) ? 8.5 : 11.0;
                page.PageSheet.PageProps.PageWidth.Value = width;
            };

            // Apply the lambda to each page in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];
                setPageWidth(i, page);
            }

            // Save the modified diagram to the output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}