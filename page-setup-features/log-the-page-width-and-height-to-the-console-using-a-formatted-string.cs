using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Retrieve page width and height (values are in inches).
                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;

                    // Log the dimensions using a formatted string.
                    Console.WriteLine($"Page dimensions - Width: {width} inches, Height: {height} inches");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}