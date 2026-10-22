using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard against missing output folder; create if necessary
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Prepare image save options with canvas size matching the page dimensions
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    // Set the page size for rendering (cast to float as required by PageSize constructor)
                    saveOptions.PageSize = new PageSize((float)pageWidth, (float)pageHeight);
                    // Export only the current page
                    saveOptions.PageIndex = i;
                    saveOptions.PageCount = 1;

                    // Define output thumbnail file name
                    string outputPath = Path.Combine(outputFolder, $"Page_{i + 1}_thumb.png");

                    // Save the thumbnail image
                    diagram.Save(outputPath, saveOptions);

                    Console.WriteLine($"Thumbnail generated for page {i + 1}: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}