using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure required arguments are provided: input file path and maximum height.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <maxHeightInInches>");
            return;
        }

        string inputPath = args[0];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse the maximum allowed page height (in inches).
        if (!double.TryParse(args[1], out double maxHeight))
        {
            Console.Error.WriteLine($"Invalid maximum height value: {args[1]}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (value is in inches).
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Compare against the predefined maximum and raise an exception if exceeded.
                    if (pageHeight > maxHeight)
                    {
                        throw new Exception($"Page height {pageHeight} inches exceeds the maximum allowed {maxHeight} inches.");
                    }
                }

                // If no exception was thrown, all pages are within the limit.
                Console.WriteLine("All page heights are within the allowed limit.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}