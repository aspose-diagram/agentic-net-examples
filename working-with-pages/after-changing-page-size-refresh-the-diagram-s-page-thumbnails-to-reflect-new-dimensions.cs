using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, newWidth, newHeight
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <newWidth> <newHeight>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string widthArg = args[2];
        string heightArg = args[3];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse width and height
        if (!double.TryParse(widthArg, out double newWidth) || !double.TryParse(heightArg, out double newHeight))
        {
            Console.Error.WriteLine("Invalid width or height values. They must be numeric.");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Change page size by setting PageWidth and PageHeight values
                page.PageSheet.PageProps.PageWidth.Value = newWidth;
                page.PageSheet.PageProps.PageHeight.Value = newHeight;

                // Refresh the diagram to update thumbnails and internal data
                diagram.Refresh();

                // Save the updated diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Page size updated and thumbnails refreshed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}