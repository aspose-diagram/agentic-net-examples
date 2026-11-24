using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output image file path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputImagePath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each layer defined on the page's sheet.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Make only the 'Presentation' layer visible; hide all others.
                        if (layer.Name != null && layer.Name.Value == "Presentation")
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure image export options (PNG format, first page only).
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    PageIndex = 0,   // Export the first page.
                    PageCount = 1    // Export a single page.
                };

                // Save the diagram snapshot as an image.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Snapshot image saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}