using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that a folder path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the folder path containing diagram files as the first argument.");
            return;
        }

        string folderPath = args[0];

        // Guard: ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Retrieve all files in the folder
        string[] allFiles = Directory.GetFiles(folderPath);

        // Process each file that has a supported Visio or export extension
        foreach (string filePath in allFiles)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            // Skip files with unsupported extensions
            if (!IsSupportedExtension(extension))
                continue;

            // Guard: ensure the file still exists before processing
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram from the file
                Diagram diagram = new Diagram(filePath);

                // Set the right footer to include a page number placeholder
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Optionally adjust the footer margin (in inches)
                diagram.HeaderFooter.FooterMargin.Value = 0.5;

                // Determine the appropriate SaveFileFormat based on the file extension
                SaveFileFormat format = GetSaveFileFormat(extension);

                // Save the modified diagram back to the original file
                diagram.Save(filePath, format);

                Console.WriteLine($"Successfully updated footer for: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during processing
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }

    // Determines whether the file extension is supported for loading/saving
    static bool IsSupportedExtension(string ext)
    {
        switch (ext)
        {
            case ".vsdx":
            case ".vsd":
            case ".vdx":
            case ".vsx":
            case ".vtx":
            case ".vstx":
            case ".vssx":
            case ".vstm":
            case ".vss":
            case ".vst":
            case ".csv":
            case ".html":
            case ".svg":
            case ".swf":
            case ".xaml":
            case ".pdf":
            case ".png":
            case ".jpeg":
            case ".jpg":
            case ".tiff":
            case ".bmp":
            case ".emf":
            case ".gif":
                return true;
            default:
                return false;
        }
    }

    // Maps a file extension to the corresponding SaveFileFormat enum value
    static SaveFileFormat GetSaveFileFormat(string ext)
    {
        switch (ext)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vstm": return SaveFileFormat.Vstm;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vst":  return SaveFileFormat.Vst;
            case ".csv":  return SaveFileFormat.Csv;
            case ".html": return SaveFileFormat.Html;
            case ".svg":  return SaveFileFormat.Svg;
            case ".swf":  return SaveFileFormat.Swf;
            case ".xaml": return SaveFileFormat.Xaml;
            case ".pdf":  return SaveFileFormat.Pdf;
            case ".png":  return SaveFileFormat.Png;
            case ".jpeg":
            case ".jpg":  return SaveFileFormat.Jpeg;
            case ".tiff": return SaveFileFormat.Tiff;
            case ".bmp":  return SaveFileFormat.Bmp;
            case ".emf":  return SaveFileFormat.Emf;
            case ".gif":  return SaveFileFormat.Gif;
            default:      return SaveFileFormat.Vsdx; // Fallback to Vsdx if unknown
        }
    }
}