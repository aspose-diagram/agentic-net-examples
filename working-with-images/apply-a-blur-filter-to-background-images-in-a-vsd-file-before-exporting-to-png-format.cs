using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command-line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input VSD/VSDX file> <output PNG file>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to find background pages
                foreach (Page page in diagram.Pages)
                {
                    // Check if the page is a background page
                    if (page.Background == BOOL.True)
                    {
                        // Iterate through all shapes on the background page
                        for (int i = 0; i < page.Shapes.Count; i++)
                        {
                            Shape shape = page.Shapes[i];

                            // Identify foreign (image) shapes
                            if (shape.Type == TypeValue.Foreign && shape.Image != null)
                            {
                                // Apply a blur filter (value range 0.0 - 1.0)
                                shape.Image.Blur.Value = 0.25;
                            }
                        }
                    }
                }

                // Configure PNG export options
                ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export hidden pages flag expects a plain bool, not BOOL
                pngOptions.ExportHiddenPage = false;

                // Save the modified diagram as PNG
                diagram.Save(outputPath, pngOptions);
            }

            Console.WriteLine($"Successfully saved blurred PNG to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}