using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the input Visio file path as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram
            Diagram sourceDiagram = new Diagram(inputPath);

            // Define the new page size (example: 8.5 x 11 inches)
            double newPageWidth = 8.5;
            double newPageHeight = 11.0;

            // Iterate over each page in the source diagram
            for (int i = 0; i < sourceDiagram.Pages.Count; i++)
            {
                // Retrieve the current page
                Page srcPage = sourceDiagram.Pages[i];

                // Create a new empty diagram for the single page export
                Diagram singlePageDiagram = new Diagram();

                // Copy all masters from the source diagram to the new diagram
                foreach (Master master in sourceDiagram.Masters)
                {
                    singlePageDiagram.Masters.Add(master);
                }

                // Add the source page to the new diagram
                // This adds a reference to the page; for a true copy, a deeper clone would be required.
                // For demonstration purposes, we use the reference and then adjust its size.
                singlePageDiagram.Pages.Add(srcPage);

                // After adding, the page will be at index 0 in the new diagram
                Page newPage = singlePageDiagram.Pages[0];

                // Resize the page to the desired dimensions
                newPage.PageSheet.PageProps.PageWidth.Value = newPageWidth;
                newPage.PageSheet.PageProps.PageHeight.Value = newPageHeight;

                // Construct the output file name using the page index (starting from 0)
                string outputFileName = $"Page_{i}.vsdx";

                // Save the single-page diagram as VSDX
                singlePageDiagram.Save(outputFileName, SaveFileFormat.Vsdx);

                Console.WriteLine($"Exported page {i} to {outputFileName}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}