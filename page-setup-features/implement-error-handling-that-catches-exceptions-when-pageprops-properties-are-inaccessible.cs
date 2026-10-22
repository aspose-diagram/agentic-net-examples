using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Attempt to read page width and height
                try
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    Console.WriteLine($"Page \"{page.Name}\": Width = {width}, Height = {height}");
                }
                catch (Exception ex)
                {
                    // Handle cases where PageProps properties are inaccessible
                    Console.Error.WriteLine($"Error accessing size for page \"{page.Name}\": {ex.Message}");
                }

                // Attempt to read the autoexpand setting
                try
                {
                    var autoExpand = page.PageSheet.PageProps.DrawingResizeType.Value;
                    Console.WriteLine($"Page \"{page.Name}\": AutoExpand = {autoExpand}");
                }
                catch (Exception ex)
                {
                    // Handle cases where the autoexpand property is inaccessible
                    Console.Error.WriteLine($"Error accessing autoexpand for page \"{page.Name}\": {ex.Message}");
                }

                Console.WriteLine(); // Blank line for readability between pages
            }
        }
        catch (Exception ex)
        {
            // General error handling for diagram loading and processing
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}