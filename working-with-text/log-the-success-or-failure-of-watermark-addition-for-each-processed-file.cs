using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // If no file paths are provided, inform the user and exit.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide at least one Visio file path as an argument.");
            return;
        }

        // Process each file path supplied via command line arguments.
        foreach (string inputPath in args)
        {
            // Guard: ensure the file exists before attempting to load.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Add a full‑page watermark to every page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Add watermark text that spans the entire page.
                    // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                    page.AddText(
                        0,                     // pinX (left)
                        0,                     // pinY (bottom)
                        pageWidth,             // width
                        pageHeight,            // height
                        "Watermark",           // watermark text
                        "Calibri",             // font name
                        "#a5a5a5",             // font color (light gray)
                        0.25);                 // font size
                }

                // Determine output file path (original name with a suffix).
                string directory = Path.GetDirectoryName(inputPath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_watermarked.vsdx");

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Log success.
                Console.WriteLine($"Processed '{inputPath}': Success. Watermarked file saved as '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // Log failure with the exception message.
                Console.Error.WriteLine($"Processed '{inputPath}': Failure. Error: {ex.Message}");
            }
        }
    }
}