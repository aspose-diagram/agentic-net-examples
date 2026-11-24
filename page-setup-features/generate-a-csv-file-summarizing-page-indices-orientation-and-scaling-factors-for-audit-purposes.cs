using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: check if the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write CSV output
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                // Write CSV header
                writer.WriteLine("PageIndex,Orientation,ScaleX,ScaleY");

                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Retrieve the page object
                    Page page = diagram.Pages[i];

                    // Get page dimensions
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Determine orientation based on dimensions
                    string orientation = width >= height ? "Landscape" : "Portrait";

                    // Retrieve scaling factors (default to 1 if not set)
                    double scaleX = page.PageSheet.PrintProps.ScaleX.Value;
                    double scaleY = page.PageSheet.PrintProps.ScaleY.Value;

                    // Write a CSV line for the current page
                    writer.WriteLine($"{i},{orientation},{scaleX},{scaleY}");
                }
            }

            Console.WriteLine($"CSV summary successfully created at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}