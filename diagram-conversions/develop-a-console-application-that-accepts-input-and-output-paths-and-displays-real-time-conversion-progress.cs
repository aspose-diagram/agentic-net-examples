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
            Console.Error.WriteLine("Usage: DiagramConverter <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Determine the desired output format based on file extension.
            string ext = Path.GetExtension(outputPath).ToLowerInvariant();

            if (ext == ".pdf")
            {
                // Use PdfSaveOptions with a page‑saving callback to report progress.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageSavingCallback = new ProgressCallback();

                // Save the diagram as PDF with progress reporting.
                diagram.Save(outputPath, pdfOptions);
            }
            else
            {
                // Map the extension to a SaveFileFormat enum value.
                SaveFileFormat format = GetSaveFileFormat(ext);
                // Save using the determined format.
                diagram.Save(outputPath, format);
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Maps a file extension to the corresponding SaveFileFormat enum value.
    /// </summary>
    private static SaveFileFormat GetSaveFileFormat(string extension)
    {
        // Default to Vsdx if the extension is unrecognized.
        switch (extension)
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vst":  return SaveFileFormat.Vst;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstm": return SaveFileFormat.Vstm;
            case ".pdf":  return SaveFileFormat.Pdf;
            case ".png":  return SaveFileFormat.Png;
            case ".jpeg":
            case ".jpg":  return SaveFileFormat.Jpeg;
            case ".bmp":  return SaveFileFormat.Bmp;
            case ".tiff":
            case ".tif":  return SaveFileFormat.Tiff;
            case ".gif":  return SaveFileFormat.Gif;
            case ".html": return SaveFileFormat.Html;
            case ".svg":  return SaveFileFormat.Svg;
            case ".swf":  return SaveFileFormat.Swf;
            case ".xps":  return SaveFileFormat.Xps;
            case ".csv":  return SaveFileFormat.Csv;
            case ".emf":  return SaveFileFormat.Emf;
            case ".xaml": return SaveFileFormat.Xaml;
            default:
                // Fallback to Vsdx for unknown extensions.
                return SaveFileFormat.Vsdx;
        }
    }
}

/// <summary>
/// Implements IPageSavingCallback to display real‑time conversion progress for PDF export.
/// </summary>
class ProgressCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        // Report the start of a page save operation.
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}...");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        // Report the completion of a page save operation.
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}.");

        // Example: stop further processing after the first page (optional).
        // args.HasMorePages = false;
    }
}