using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the original Visio file (any supported format)
            string inputPath = "original.vdx";

            // Path for the new file that will preserve the original format
            string outputPath = "updated.vdx";

            // Load the diagram from the original file
            Diagram diagram = new Diagram(inputPath);

            // Determine the original file format based on its extension
            SaveFileFormat originalFormat = GetSaveFileFormatFromExtension(Path.GetExtension(inputPath));

            // Save the diagram to the new file using the same format
            diagram.Save(outputPath, originalFormat);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to map file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFileFormatFromExtension(string extension)
    {
        if (string.IsNullOrEmpty(extension))
            return SaveFileFormat.Vdx; // Default fallback

        switch (extension.Trim().ToLowerInvariant())
        {
            case ".vsdx":
                return SaveFileFormat.Vsdx;
            case ".vstx":
                return SaveFileFormat.Vstx;
            case ".vssx":
                return SaveFileFormat.Vssx;
            case ".vsdm":
                return SaveFileFormat.Vsdm;
            case ".vssm":
                return SaveFileFormat.Vssm;
            case ".vsx":
                return SaveFileFormat.Vsx;
            case ".vdx":
                return SaveFileFormat.Vdx;
            case ".vtx":
                return SaveFileFormat.Vtx;
            case ".pdf":
                return SaveFileFormat.Pdf;
            case ".png":
                return SaveFileFormat.Png;
            case ".jpeg":
            case ".jpg":
                return SaveFileFormat.Jpeg;
            case ".tiff":
            case ".tif":
                return SaveFileFormat.Tiff;
            case ".bmp":
                return SaveFileFormat.Bmp;
            case ".emf":
                return SaveFileFormat.Emf;
            case ".gif":
                return SaveFileFormat.Gif;
            case ".html":
                return SaveFileFormat.Html;
            case ".svg":
                return SaveFileFormat.Svg;
            case ".xps":
                return SaveFileFormat.Xps;
            case ".swf":
                return SaveFileFormat.Swf;
            case ".xaml":
                return SaveFileFormat.Xaml;
            default:
                // If the extension is unrecognized, default to VDX
                return SaveFileFormat.Vdx;
        }
    }
}
