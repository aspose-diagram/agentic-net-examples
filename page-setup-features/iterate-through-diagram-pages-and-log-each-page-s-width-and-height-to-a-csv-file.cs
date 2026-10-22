using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output CSV path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input diagram file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);

            // Open CSV writer
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("PageIndex,Width,Height");

                // Iterate through pages and log dimensions
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page width and height (values are in inches)
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Write a CSV line for the current page
                    writer.WriteLine($"{pageIndex},{width},{height}");

                    pageIndex++;
                }
            }

            Console.WriteLine($"Page dimensions have been written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Dispose the diagram if it implements IDisposable
            if (diagram != null)
            {
                diagram.Dispose();
            }
        }
    }
}