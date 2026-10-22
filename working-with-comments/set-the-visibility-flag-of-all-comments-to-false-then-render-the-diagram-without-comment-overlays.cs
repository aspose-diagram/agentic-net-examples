using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputImageFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path. If not supplied, create a PNG file beside the input.
        string outputPath = args.Length >= 2
            ? args[1]
            : Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_no_comments.png");

        try
        {
            // Load the diagram.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and attempt to hide comments.
                foreach (Page page in diagram.Pages)
                {
                    // Access page-level annotations (comments).
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // If a visibility property existed, it would be set here.
                        // The API does not expose a visibility flag for Annotation.
                    }
                }

                // Configure image export options to exclude comment overlays.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // IsExportComments expects a bool, so assign false directly.
                saveOptions.IsExportComments = false;

                // Save the rendered diagram as an image without comments.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram rendered successfully without comments: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
