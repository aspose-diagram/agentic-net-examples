using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path (optional second argument, otherwise overwrite input)
        string outputPath = args.Length > 1 ? args[1] : inputPath;

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Minimum page height in inches
            const double minHeightInches = 5.0;

            // Iterate through all pages and enforce the minimum height
            foreach (Page page in diagram.Pages)
            {
                // Retrieve current page height
                double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                // If the page height is less than the minimum, set it to the minimum
                if (currentHeight < minHeightInches)
                {
                    page.PageSheet.PageProps.PageHeight.Value = minHeightInches;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}