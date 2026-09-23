using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect a file path as the first argument.
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the path to a Visio file (vsdx or vdx).");
            return;
        }

        // Assign the input path and verify the file exists.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine the file extension (lowercase) to decide which font to apply.
        string extension = Path.GetExtension(inputPath).ToLowerInvariant();

        Diagram diagram;
        try
        {
            // Load the diagram using the appropriate constructor.
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Access the global footer font configuration.
        var footerFont = diagram.HeaderFooter.HeaderFooterFont;

        // Apply font settings based on the file extension.
        if (extension == ".vsdx")
        {
            // For VSDX files use Calibri, bold (weight 700), 12‑point size.
            footerFont.FaceName = "Calibri";
            footerFont.Weight = 700;               // Bold weight.
            footerFont.Height = 12;                // Point size.
            footerFont.Italic = BOOL.False;        // No italic.
            footerFont.Underline = BOOL.False;     // No underline.
        }
        else if (extension == ".vdx")
        {
            // For VDX files use Arial, regular weight, 10‑point size.
            footerFont.FaceName = "Arial";
            footerFont.Weight = 400;               // Regular weight.
            footerFont.Height = 10;                // Point size.
            footerFont.Italic = BOOL.False;        // No italic.
            footerFont.Underline = BOOL.False;     // No underline.
        }
        else
        {
            Console.WriteLine("Unsupported file extension. Only .vsdx and .vdx are handled.");
            return;
        }

        // Prepare an output file name indicating the modification.
        string directory = Path.GetDirectoryName(inputPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputFileName = $"{fileNameWithoutExt}_modified{extension}";
        string outputPath = Path.Combine(directory, outputFileName);

        try
        {
            // Save the diagram using the same format as the original.
            if (extension == ".vsdx")
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            else // .vdx
            {
                diagram.Save(outputPath, SaveFileFormat.Vdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            return;
        }

        Console.WriteLine($"Diagram saved with updated footer font: {outputPath}");
    }
}