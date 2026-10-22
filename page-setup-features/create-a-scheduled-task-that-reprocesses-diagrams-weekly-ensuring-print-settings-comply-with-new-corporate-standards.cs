using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <folderPath>");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Supported Visio file extensions
        string[] extensions = new[]
        {
            ".vsdx", ".vsd", ".vdx", ".vsx", ".vssx",
            ".vss", ".vstx", ".vst", ".vtx"
        };

        // Iterate over files in the folder
        foreach (string filePath in Directory.GetFiles(folderPath))
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (Array.IndexOf(extensions, ext) < 0)
                continue; // Skip unsupported files

            // Guard: ensure the file exists (redundant but per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Update print settings for each page to meet corporate standards
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps collection
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Set orientation to Landscape
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Set scaling to 75% (direct double assignment)
                    printProps.ScaleX.Value = 0.75;
                    printProps.ScaleY.Value = 0.75;

                    // Enable fit-to-sheet (OnPage) and set pages per sheet to 1x1
                    printProps.OnPage.Value = BOOL.True;
                    printProps.PagesX.Value = 1;
                    printProps.PagesY.Value = 1;

                    // Set uniform margins of 0.5 inches (36 points) using double values
                    double margin = 36; // points
                    printProps.PageTopMargin.Value = margin;
                    printProps.PageBottomMargin.Value = margin;
                    printProps.PageLeftMargin.Value = margin;
                    printProps.PageRightMargin.Value = margin;
                }

                // Determine appropriate save format based on original extension
                SaveFileFormat format = GetSaveFormat(ext);

                // Overwrite the original file with updated settings
                diagram.Save(filePath, format);

                Console.WriteLine($"Processed and saved: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors encountered during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension)
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vsd":
                return SaveFileFormat.Vsd;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vss":
                return SaveFileFormat.Vss;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vst":
                return SaveFileFormat.Vst;
            case ".vtx":
                return SaveFileFormat.Vtx;
            default:
                // Default to Vsdx if unknown; callers should ensure extension is supported
                return SaveFileFormat.Vsdx;
        }
    }
}
