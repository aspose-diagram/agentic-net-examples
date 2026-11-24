using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output image path as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputImagePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path; default to same folder with .png extension.
        string outputPath = args.Length >= 2 ? args[1] : Path.ChangeExtension(inputPath, ".png");
        string outputDir = Path.GetDirectoryName(outputPath);
        // Guard: ensure output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate all pages and their annotations (comments).
                foreach (Page page in diagram.Pages)
                {
                    // Access the collection of annotations for the current page.
                    foreach (Annotation comment in page.PageSheet.Annotations)
                    {
                        // No visibility property exists for Annotation; comments will be hidden by export options.
                    }
                }

                // Create image save options for PNG format.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Disable exporting of comments (comment overlays) in the rendered image.
                saveOptions.IsExportComments = false;

                // Save the diagram as an image without comment overlays.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Diagram rendered successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
