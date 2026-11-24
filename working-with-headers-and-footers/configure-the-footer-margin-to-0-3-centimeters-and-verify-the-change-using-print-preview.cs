using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPreviewImage>");
            return;
        }

        string inputPath = args[0];
        string previewPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block to ensure proper disposal.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Set the footer margin to 0.3 centimeters.
                // Visio stores margins in inches; 0.3 cm = 0.11811 inches.
                // -----------------------------------------------------------------
                diagram.HeaderFooter.FooterMargin.Value = 0.11811;

                // Verify the change by writing the new margin value to the console.
                Console.WriteLine($"Footer margin set to {diagram.HeaderFooter.FooterMargin.Value} inches (≈0.3 cm).");

                // -----------------------------------------------------------------
                // Generate a preview image of the first page to visually verify the
                // footer margin. Export the first page as a PNG image using ImageSaveOptions.
                // -----------------------------------------------------------------
                ImageSaveOptions previewOptions = new ImageSaveOptions(SaveFileFormat.Png)
                {
                    // Export only the first page.
                    PageIndex = 0,
                    PageCount = 1
                };
                diagram.Save(previewPath, previewOptions);
                Console.WriteLine($"Print preview image saved to \"{previewPath}\".");

                // -----------------------------------------------------------------
                // Optionally, save the modified diagram to a new file.
                // -----------------------------------------------------------------
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputPath) + "_Modified.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}