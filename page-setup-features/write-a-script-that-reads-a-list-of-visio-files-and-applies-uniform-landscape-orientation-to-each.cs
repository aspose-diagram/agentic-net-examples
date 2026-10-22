using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate that at least one file path is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide one or more Visio file paths as command-line arguments.");
            return;
        }

        // Process each file path supplied via command line.
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
                // Load the Visio diagram from the file.
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and set the print orientation to Landscape.
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Determine the appropriate SaveFileFormat based on the original file extension.
                string extension = Path.GetExtension(inputPath).ToLowerInvariant();
                SaveFileFormat format = GetSaveFileFormat(extension);

                // Construct the output path (overwrite the original file).
                string outputPath = inputPath;

                // Save the modified diagram using the determined format.
                diagram.Save(outputPath, format);

                // Dispose the diagram to release resources.
                diagram.Dispose();

                Console.WriteLine($"Successfully updated orientation for: {inputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// </summary>
    /// <param name="extension">File extension including the leading dot (e.g., ".vsdx").</param>
    /// <returns>SaveFileFormat value; defaults to Vsdx if unknown.</returns>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        // Note: The SaveFileFormat enum uses PascalCase member names.
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vsdm":
                return SaveFileFormat.Vsdm;
            case ".vssm":
                return SaveFileFormat.Vssm;
            case ".vstm":
                return SaveFileFormat.Vstm;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".html":
                return SaveFileFormat.Html;
            case ".mmd":
                // MMD files can be saved as Vsdx after processing.
                return SaveFileFormat.Vsdx;
            default:
                // Fallback to Vsdx for unrecognized extensions.
                return SaveFileFormat.Vsdx;
        }
    }
}