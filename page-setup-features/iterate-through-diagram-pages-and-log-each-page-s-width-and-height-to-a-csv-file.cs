using System;
using System.IO;
using System.Globalization;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command-line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Open a StreamWriter for the CSV output.
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header.
                    writer.WriteLine("PageIndex,WidthInches,HeightInches");

                    int pageIndex = 0;
                    // Iterate through each page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        pageIndex++;

                        // Retrieve page width and height (values are in inches).
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        // Write the data line using invariant culture to ensure consistent decimal format.
                        string line = string.Format(CultureInfo.InvariantCulture, "{0},{1},{2}", pageIndex, width, height);
                        writer.WriteLine(line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}