using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidthInches = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeightInches = page.PageSheet.PageProps.PageHeight.Value;

                    // Configure image save options for PNG thumbnail
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        // Export only the current page
                        PageIndex = pageIndex,
                        PageCount = 1,
                        // Define canvas size using the page dimensions
                        PageSize = new PageSize((float)pageWidthInches, (float)pageHeightInches)
                    };

                    // Build output file name for the thumbnail
                    string outputPath = $"thumbnail_page_{pageIndex + 1}.png";

                    // Save the thumbnail image for the current page
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Generated thumbnail: {outputPath}");

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}